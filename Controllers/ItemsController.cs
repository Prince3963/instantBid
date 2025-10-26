using instantBid.DTOs;
using instantBid.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace instantBid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemServiceInterface itemService;
        public ItemsController(IItemServiceInterface itemService)
        {
            this.itemService = itemService;
        }

        [HttpPost]
        [Route("/addItem")]
        public async Task<IActionResult> addItems([FromForm] ItemsDTO itemsDTO)
        {
            var result = await itemService.addItem(itemsDTO);
            return Ok(result);  
        }

        [HttpGet]
        [Route("/getItem")]
        public async Task<IActionResult> getItems()
        {
            var result = await itemService.getAllItem();
            return Ok(result);
        }
    }
}
