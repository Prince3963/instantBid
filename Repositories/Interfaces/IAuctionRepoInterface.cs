using instantBid.Models;

namespace instantBid.Repositories.Interfaces
{
    public interface IAuctionRepoInterface
    {
        Task<Auction> insertAuction(Auction auction);
    }
}
