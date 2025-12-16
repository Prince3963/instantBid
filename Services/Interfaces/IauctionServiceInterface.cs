using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Models;

namespace instantBid.Services.Interfaces
{
    public interface IauctionServiceInterface
    {
        Task<ServiceResponses<string>> addAuction(AuctionDTO auctionDTO);
        Task<ServiceResponses<List<AuctionDTO>>> GetAllAuctions();
        Task<ServiceResponses<Auction>> GetAuctionById(int id);
        Task<ServiceResponses<List<AuctionDTO>>> searchAuction(string auction);
        Task<ServiceResponses<CurrentBidDTO?>> getCurrentBidByAuctionId(int id);
        Task<ServiceResponses<string>> updateAuctionStatus(int auctioId, bool isActive);
    }
}
