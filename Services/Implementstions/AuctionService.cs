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
        private readonly IItemRepoInterface itemRepo;
        private readonly CloudinaryService cloudinaryService;
        public AuctionService(IItemRepoInterface itemRepo, CloudinaryService cloudinaryService, IAuctionRepoInterface auctionRepo)
        {
            this.auctionRepo = auctionRepo;
            this.itemRepo = itemRepo;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<ServiceResponses<string>> addAuction(AuctionDTO auctionDTO)
        {
            var response = new ServiceResponses<string>();
            try
            {
                // Upload image
                var imageURL = auctionDTO.ItemImage != null
                    ? await cloudinaryService.uploadImages(auctionDTO.ItemImage)
                    : null;

                // Create Item
                var newItem = new Items
                {
                    ItemName = auctionDTO.ItemName,
                    ItemDescription = auctionDTO.ItemDescription,
                    ItemImage = imageURL,
                    Status = true,
                    CreatedAt = DateTime.Now,
                    UserId = auctionDTO.UserId
                };

                // Save Item
                await itemRepo.addItem(newItem);

                // Create Auction
                var auction = new Auction
                {
                    ItemId = newItem.ItemId,
                    AuctionItemName = auctionDTO.AuctionItemName,
                    AuctionStartTime = auctionDTO.AuctionStartTime?.TimeOfDay,
                    AuctionEndTime = auctionDTO.AuctionEndTime?.TimeOfDay,
                    StartingBid = auctionDTO.StartingBid,
                    EndingBid = auctionDTO.CurrentBid,
                    CreatedAt = auctionDTO.CreatedAt ?? DateTime.Now,
                    Status = auctionDTO.Status ?? true,
                    UserId = auctionDTO.UserId
                };

                // Save Auction
                await auctionRepo.insertAuction(auction);

                response.data = "1";
                response.message = "Auction & Item Added Successfully";
                response.status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.data = "0";
                response.message = "Error adding auction & item: " + ex.Message;
                response.status = false;
                return response;
            }
        }

        // Get all auctions with items
        public async Task<ServiceResponses<List<AuctionDTO>>> GetAllAuctions()
        {
            var response = new ServiceResponses<List<AuctionDTO>>();
            try
            {
                var auctions = await auctionRepo.GetAllAuctions();

                if (auctions == null || auctions.Count == 0)
                {
                    response.data = new List<AuctionDTO>(); // Ensure it is an empty list, not null
                    response.message = "No auctions available";
                    response.status = false;
                    return response;
                }

                var result = auctions.Select(a => new AuctionDTO
                {
                    AuctionId = a.AuctionId,
                    ItemName = a.Items?.ItemName,
                    ItemDescription = a.Items?.ItemDescription,
                    ItemImageURL = a.Items?.ItemImage,
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
                    UserId = a.UserId
                }).ToList();

                response.data = result;
                response.message = "Auctions fetched successfully";
                response.status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.data = null;
                response.message = "Error fetching auctions: " + ex.Message;
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

        public async Task<ServiceResponses<CurrentBidDTO?>> getCurrentBidByAuctionId(int id)
        {
            var response = new ServiceResponses<CurrentBidDTO>();
            try
            {
                var auction = await auctionRepo.getAuctionByIDAndCurrentBid(id);
                var currentBid = new CurrentBidDTO
                {
                    AuctionId = auction.AuctionId,
                    AuctionItemName = auction.AuctionItemName,
                    EndingBid = auction.EndingBid,
                };

                response.data = currentBid;
                response.message = "Auction Current Bid is";
                response.status = true;

                return response;
            }
            catch(Exception ex)
            {
                response.data=null;
                response.message = "Please check your services";
                response.status = false;

                return response;
            }

        }

        public async Task<ServiceResponses<List<AuctionDTO>>> searchAuction(string auction)
        {
            var response = new ServiceResponses<List<AuctionDTO>>();
            try
            {
                var auctions = await auctionRepo.searchAuction(auction);
                if (auctions == null || auctions.Count == 0 )
                {
                    response.data = new List<AuctionDTO>();
                    response.message = "Auction not available";
                    response.status = false;

                    return response;
                }
                var result = auctions.Select(a => new AuctionDTO
                {
                    AuctionId = a.AuctionId,
                    ItemName = a.Items?.ItemName,
                    ItemDescription = a.Items?.ItemDescription,
                    ItemImageURL = a.Items?.ItemImage,
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
                    CreatedAt = a.CreatedAt
                }).ToList();

                response.data = result;
                response.message = "Auction searched";
                response.status = true;
                return response;
            }
            catch(Exception ex)
            {
                response.data = null;
                response.message = "Check Auction Service " + ex.Message;
                response.status = false;
                return response;
            }
        }
    }
}
