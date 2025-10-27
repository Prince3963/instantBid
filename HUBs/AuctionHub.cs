using instantBid.DBContext;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace instantBidBackend.Hubs
{
    public class AuctionHub : Hub
    {
        private readonly AppDbContext _db;
        public AuctionHub(AppDbContext db)
        {
            _db = db;
        }

        public async Task JoinAuction(int auctionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"auction-{auctionId}");
        }

        public async Task PlaceBid(int auctionId, decimal amount, string userName)
        {
            var auction = await _db.Auctions.FirstOrDefaultAsync(a => a.AuctionId == auctionId);

            if (auction == null)
            {
                await Clients.Caller.SendAsync("BidRejected", "Auction not found");
                return;
            }

            // ✅ Get current server time in TimeSpan (for model compatibility)
            var currentTime = DateTime.Now.TimeOfDay;

            // ✅ Auction end time check
            if (auction.AuctionEndTime.HasValue && auction.AuctionEndTime.Value <= currentTime)
            {
                await Clients.Caller.SendAsync("BidRejected", "Auction ended");
                return;
            }

            // ✅ Check if bid is lower than current
            if (auction.EndingBid.HasValue && amount <= auction.EndingBid.Value)
            {
                await Clients.Caller.SendAsync("BidRejected", "Bid too low");
                return;
            }

            // ✅ Update current bid
            auction.EndingBid = (int)amount;
            await _db.SaveChangesAsync();

            // ✅ Notify all users in that auction room
            await Clients.Group($"auction-{auctionId}")
                .SendAsync("BidPlaced", new
                {
                    AuctionId = auctionId,
                    Amount = amount,
                    User = userName
                });
        }
    }
}
