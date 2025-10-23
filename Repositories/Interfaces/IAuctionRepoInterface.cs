using instantBid.DTOs;
using instantBid.Models;

namespace instantBid.Repositories.Interfaces
{
    public interface IAuctionRepoInterface
    {
        Task<Auction> insertAuction(Auction auction);
        Task<List<Auction>> GetAllAuctions();
        Task<Auction?> GetAuctionById(int id);

    }
}
