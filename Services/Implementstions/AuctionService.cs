using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Models;
using instantBid.Repositories.Interfaces;
using instantBid.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace instantBid.Services.Implementstions
{
    public class AuctionService : IauctionServiceInterface
    {
        private readonly IAuctionRepoInterface auctionRepo;
        public AuctionService(IAuctionRepoInterface auctionRepo)
        {
            this.auctionRepo = auctionRepo;
        }

        public async Task<ServiceResponses<string>> addAuction(AuctionDTO auctionDTO)
        {
            var response = new ServiceResponses<string>();
            var auction = new Auction
            {
                AuctionItemName = auctionDTO.AuctionItemName,
                AuctionStartTime = auctionDTO.AuctionStartTime?.TimeOfDay,
                AuctionEndTime = auctionDTO.AuctionEndTime?.TimeOfDay,
                StartingBid = auctionDTO.StartingBid,
                EndingBid = auctionDTO.CurrentBid,
                CreatedAt = auctionDTO.CreatedAt ?? DateTime.Now,
                Status = auctionDTO.Status ?? true,
                UserId = auctionDTO.UserId,
            };

            await auctionRepo.insertAuction(auction);

            response.data = "1";
            response.message = "Auction Added";
            response.status = true;

            return response;
        }


        public async Task<ServiceResponses<List<AuctionDTO>>> GetAllAuctions()
        {
            var response = new ServiceResponses<List<AuctionDTO>>();
            try
            {
                var auction = await auctionRepo.GetAllAuctions();

                if (auction == null || auction.Count == 0)
                {
                    response.data = null;
                    response.message = "Auction is not available";
                    response.status = false;

                    return response;
                }

                var result = auction.Select(a => new AuctionDTO
                {
                    AuctionItemName = a.AuctionItemName,
                    AuctionStartTime = a.AuctionStartTime.HasValue && a.CreatedAt.HasValue
        ? a.CreatedAt.Value.Date.Add(a.AuctionStartTime.Value)
        : (DateTime?)null,
                    AuctionEndTime = a.AuctionEndTime.HasValue && a.CreatedAt.HasValue
        ? a.CreatedAt.Value.Date.Add(a.AuctionEndTime.Value)
        : (DateTime?)null,
                    StartingBid = a.StartingBid,
                    CurrentBid = a.EndingBid,
                    Status = a.Status,
                    CreatedAt = a.CreatedAt,
                    //UserId = a.UserId
                }).ToList();

                response.data = result;
                response.message = "Auctions are available";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.data = null;
                response.message = "Check your Auction Service";
                response.status = false;

                return response;
            }
        }

        public async Task<ServiceResponses<Auction>> GetAuctionById(int id)
        {
            var response = new ServiceResponses<Auction>();

            try
            {
                var auctionById = await auctionRepo.GetAuctionById(id);

                if (auctionById == null)
                {
                    response.data = null;
                    response.message = "Auction not found.";
                    response.status = false;
                }

                response.data = auctionById;
                response.message = "Auction found.";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.data = null;
                response.message = "Check your Auction Service";
                response.status = false;

                return response;
            }
        }
    }
}
