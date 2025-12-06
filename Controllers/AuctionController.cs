using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Services.Implementstions;
using instantBid.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace instantBid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IauctionServiceInterface auctionService;
        public AuctionController(IauctionServiceInterface auctionService)
        {
            this.auctionService = auctionService;
        }

        [HttpPost]
        [Route("/AddAuction")]
        public async Task<IActionResult> addAuction([FromForm] AuctionDTO auctionDTO)
        {
            try
            {
                var result = await auctionService.addAuction(auctionDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound("Check User Controller..." + ex.Message);
            }
            finally
            {
                Console.WriteLine("Successful");
            }
        }

        [HttpGet]
        [Route("/search")]
        public async Task<IActionResult> searchAuction(string auction)
        {
            if (auction == null)
            {
                return NotFound("This Item not found for an auction");
            }
            var result = await auctionService.searchAuction(auction);
            return Ok(result);
        }

        [HttpGet]
        [Route("/getAuctions")]
        public async Task<IActionResult> getAllAuction()
        {
            var result = await auctionService.GetAllAuctions();
            return Ok(result);
        }

        [HttpGet]
        [Route("/getAuctionById/{id}")]
        public async Task<IActionResult> getAuctionById(int id)
        {
            var result = await auctionService.GetAuctionById(id);
            if (result == null)
            {
                return NotFound("Auction not found check Controller ");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("/CurrentBidOfAuction")]
        public async Task<IActionResult> getCurrentBid(int id)
        {
            var result = await auctionService.getCurrentBidByAuctionId(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
