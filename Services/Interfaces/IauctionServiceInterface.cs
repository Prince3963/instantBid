using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Models;

namespace instantBid.Services.Interfaces
{
    public interface IauctionServiceInterface
    {
        Task<ServiceResponses<string>> addAuction(AuctionDTO auctionDTO);
        Task<ServiceResponses<List<Auction>>> GetAllAuctions();
        Task<ServiceResponses<Auction>> GetAuctionById(int id);

    }
}
