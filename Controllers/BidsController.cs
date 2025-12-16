using instantBid.Services.Implementstions;
using instantBid.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace instantBid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IBidServiceInterface bidServices;
        public BidsController(IBidServiceInterface bidServices)
        {
            this.bidServices = bidServices;
        }

        [HttpGet]
        public async Task<IActionResult> getAllBids()
        {
            var result = await bidServices.getAllBids();
            if (result == null)
            {
                return NotFound("Bids not available");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("/getBidByAuction")]
        public async Task<IActionResult> getBidByAuction(int id)
        {
            var result = await bidServices.getBidByAuctionId(id);
            if (result == null)
            {
                return NotFound("No bid yet for this auction");
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("/getBidByUser")]
        public async Task<IActionResult> getBidByUser([FromQuery] int id)
        {
            var result = await bidServices.getBidByUser(id);
            if (result == null)
            {
                return NotFound("User have no bid history");
            }
            return Ok(result);
        }
    }
}
