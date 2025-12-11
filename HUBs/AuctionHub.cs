using System.Security.Claims;
using instantBid.DBContext;
using instantBid.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace instantBidBackend.Hubs
{
    public class AuctionHub : Hub
    {
        private readonly AppDbContext dbContext;

        public AuctionHub(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // ✅ Join auction group
        public async Task JoinAuction(int auctionId)
        {
            var userId = Context.User?.FindFirst("UserId")?.Value; // JWT se aayega
            if (userId != null)
            {
                var groupName = $"auction-{auctionId}";
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                Console.WriteLine($"? User joined auction: {auctionId}, User: {userId}");
            }
        }

        public override async Task OnConnectedAsync()
        {
            var user = Context.User?.Identity?.Name ?? "Anonymous";
            Console.WriteLine($"✅ New connection: {Context.ConnectionId}, User: {user}");
            await base.OnConnectedAsync();
        }

        // ✅ Place bid
        public async Task PlaceBid(int auctionId, decimal amount)
        {
            var userIdClaim = Context.User?.FindFirst("UserId")?.Value;
            var userNameClaim = Context.User?.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                await Clients.Caller.SendAsync("BidRejected", "Unauthorized user");
                return;
            }

            int userId = int.Parse(userIdClaim);
            string userName = userNameClaim ?? "Unknown User";

            var auction = await dbContext.Auctions.FirstOrDefaultAsync(a => a.AuctionId == auctionId);
            if (auction == null)
            {
                await Clients.Caller.SendAsync("BidRejected", "Auction not found");
                return;
            }

            // Auction time check
            var currentTime = DateTime.Now.TimeOfDay;
            if (auction.AuctionEndTime.HasValue && auction.AuctionEndTime.Value <= currentTime)
            {
                await Clients.Caller.SendAsync("BidRejected", "Auction ended");
                return;
            }

            if (auction.EndingBid.HasValue && amount <= auction.EndingBid.Value)
            {
                await Clients.Caller.SendAsync("BidRejected", "Bid too low");
                return;
            }

            // Update Ending Bid
            auction.EndingBid = (int)amount;

            // Save Bid History
            dbContext.BidHistories.Add(new BidHistory
            {
                AuctionId = auctionId,
                UserId = userId,
                BidAmount = amount,
                BidTime = DateTime.Now
            });

            await dbContext.SaveChangesAsync();

            // Notify all users in group
            var groupName = $"auction-{auctionId}";
            await Clients.Group(groupName)
                .SendAsync("BidPlaced", new
                {
                    AuctionId = auctionId,
                    Amount = amount,
                    User = userName
                });
        }


        // ✅ Get bid history
        public async Task GetBidHistory(int auctionId)
        {
            var history = await dbContext.BidHistories
                .Include(b => b.User)
                .Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.BidTime)
                .Select(b => new
                {
                    b.BidAmount,
                    b.BidTime,
                    UserName = b.User.Name
                })
                .ToListAsync();

            await Clients.Caller.SendAsync("ReceiveBidHistory", history);
        }
    }
}
