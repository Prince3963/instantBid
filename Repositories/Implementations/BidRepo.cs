using instantBid.DBContext;
using instantBid.Models;
using instantBid.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace instantBid.Repositories.Implementations
{
    public class BidRepo : IBidRepoInterface
    {
        private readonly AppDbContext dbContext;
        public BidRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<BidHistory>> GetAllBids()
        {
            var result = await dbContext.BidHistories
                .Include(b => b.User)
                .Include(b => b.Auction)
                .ThenInclude(i => i.Items)
                .ToListAsync();

            return result;
        }

        public async Task<List<BidHistory>> getBidsByAuctionId(int id)
        {
            var result = await dbContext.BidHistories
                .Where(b => b.AuctionId == id)
                .Include(b => b.Auction)
                .Include(b => b.User)
                .ToListAsync();

            return result;
        }

        public async Task<List<BidHistory>> getBidsByUser(int id)
        {
            var result = await dbContext.BidHistories
                .Where(b => b.UserId == id)
                .Include(b => b.User)
                .Include(b => b.Auction)
                .ThenInclude(a => a.Items)

                .ToListAsync();

            return result;
        }
    }
}
