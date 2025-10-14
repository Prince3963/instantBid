using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Models;
using instantBid.Repositories.Interfaces;
using instantBid.Services.Interfaces;

namespace instantBid.Services.Implementstions
{
    public class AuctionService : IauctionServiceInterface
    {
        private readonly IAuctionRepoInterface auctionRepoInterface;
        public AuctionService(IAuctionRepoInterface auctionRepoInterface)
        {
            this.auctionRepoInterface = auctionRepoInterface;
        }

        public async Task<ServiceResponses<string>> addAuction(AuctionDTO auctionDTO)
        {
            var response = new ServiceResponses<string>();
            var auction = new Auction
            {
                AuctionItemName = auctionDTO.AuctionItemName,
                AuctionStartTime = auctionDTO.AuctionStartTime,
                AuctionEndTime = auctionDTO.AuctionEndTime,
                StartingBid = auctionDTO.StartingBid,
                EndingBid = auctionDTO.EndingBid,
                CreatedAt = auctionDTO.CreatedAt,
                Status = auctionDTO.Status,
                UserId = auctionDTO.UserId,
            };

            await auctionRepoInterface.insertAuction(auction);

            response.data = "1";
            response.message = "Auction Added";
            response.status = true;

            return response;
        }
    }
}
