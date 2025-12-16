using instantBid.Models;

namespace instantBid.Repositories.Interfaces
{
    public interface IBidRepoInterface
    {
        public Task<List<BidHistory>> GetAllBids();
        public Task<List<BidHistory>> getBidsByAuctionId(int id);
        public Task<List<BidHistory>> getBidsByUser(int id);
    }
}
