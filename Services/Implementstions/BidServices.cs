using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Repositories.Interfaces;
using instantBid.Services.Interfaces;

namespace instantBid.Services.Implementstions
{
    public class BidServices : IBidServiceInterface
    {
        private readonly IBidRepoInterface bidRepo;
        public BidServices(IBidRepoInterface bidRepo)
        {
            this.bidRepo = bidRepo;
        }

        public async Task<ServiceResponses<List<BidHistoryDTO>>> getAllBids()
        {
            var response = new ServiceResponses<List<BidHistoryDTO>>();
            try
            {
                var bids = await bidRepo.GetAllBids();
                if (!bids.Any())
                {
                    response.data = null;
                    response.message = "No bids Available";
                    response.status = false;
                    return response;
                }

                var result = bids.Select(b => new BidHistoryDTO
                {
                    AuctionId = b.AuctionId,
                    UserId = b.UserId,
                    AuctionItemName = b.Auction?.AuctionItemName,
                    ItemImage = b.Auction?.Items?.ItemImage,
                    Name = b.User?.Name,
                    BidAmount = b.BidAmount,
                    BidTime = b.BidTime,
                    CurrentBid = b.Auction?.EndingBid
                }).ToList();


                response.data = result;
                response.message = "Bids of an All Auctions";
                response.status = true;
                return response;

            }
            catch (Exception ex)
            {
                response.data = null;
                response.message = ex.ToString();
                response.status = false;

                return response;
            }
        }

        public async Task<ServiceResponses<List<BidHistoryDTO>>> getBidByAuctionId(int id)
        {
            var response = new ServiceResponses<List<BidHistoryDTO>>();
            try
            {
                var bids = await bidRepo.getBidsByAuctionId(id);
                if (bids == null)
                {
                    response.data = null;
                    response.message = "No bids availabel for this auction";
                    response.status = false;
                    return response;
                }

                var result = bids.Select(b => new BidHistoryDTO
                {
                    //AuctionId = b.AuctionId,
                    AuctionItemName = b.Auction?.AuctionItemName,
                    BidAmount = b.BidAmount,
                    BidTime = b.BidTime,
                    //CurrentBid= b.Auction?.EndingBid,
                    Name = b.User?.Name
                }).ToList();

                response.data = result;
                response.message = "Your Bids according to the auctions";
                response.status = true;
                return response;

            }catch(Exception ex)
            {
                response.data = null;
                response.message = ex.ToString();
                response.status = false;
                return response;
            }
        }

        public async Task<ServiceResponses<List<BidHistoryDTO>>> getBidByUser(int id)
        {
            var response = new ServiceResponses<List<BidHistoryDTO>>();
            try
            {
                var existUser = await bidRepo.getBidsByUser(id);
                if (!existUser.Any())
                {
                    response.data = new List<BidHistoryDTO>();
                    response.message = "User have no bid history";
                    response.status = true;
                    return response;
                }

                var result = existUser.Select(b => new BidHistoryDTO
                {
                    AuctionId = b.AuctionId,
                    AuctionItemName = b.Auction?.AuctionItemName,
                    CurrentBid = b.Auction?.EndingBid,
                    ItemImage = b.Auction?.Items?.ItemImage,

                    UserId = b.UserId,
                    Name = b.User?.Name,

                    BidAmount = b.BidAmount,
                    BidTime = b.BidTime
                }).ToList();

                response.data = result;
                response.message = "Users bid";
                response.status = true;

                return response;

            }
            catch(Exception ex)
            {
                response.data = null;
                response.message = ex.ToString();
                response.status = false;
                return response;
            }
        }
    }
}
