using instantBid.DTOs;
using instantBid.HelperServices;

namespace instantBid.Services.Interfaces
{
    public interface IauctionServiceInterface
    {
        Task<ServiceResponses<string>> addAuction(AuctionDTO auctionDTO);
    }
}
