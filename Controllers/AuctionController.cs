using instantBid.DTOs;
using instantBid.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
    }
}
