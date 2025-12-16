using instantBid.DTOs;
using instantBid.HelperServices;

namespace instantBid.Services.Interfaces
{
    public interface IBidServiceInterface
    {
        Task<ServiceResponses<List<BidHistoryDTO>>> getAllBids();
        Task<ServiceResponses<List<BidHistoryDTO>>> getBidByAuctionId(int id);
        Task<ServiceResponses<List<BidHistoryDTO>>> getBidByUser (int id);
    }
}
