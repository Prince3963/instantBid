using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace instantBid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServiceInterface userInterface;
        public UsersController(IUserServiceInterface userInterface)
        {
            this.userInterface = userInterface;
        }

        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> getAllUser([FromQuery] RegistrationDTO userDTO)
        {
            var response = new ServiceResponses<string>();
            try
            {
                var result = await userInterface.getUsers(userDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                response.data = "0";
                response.message = " Check User Controller..." + ex.Message;
                response.status = false;

                return NotFound();
            }
        }


        [HttpPost]
        [Route("RegisterUser")]
        public async Task<ActionResult> RegisterUser([FromForm] RegistrationDTO registrationDTO)
        {
            try
            {
                var result = await userInterface.registerUser(registrationDTO);
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

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> loginUser([FromForm] LoginDTO loginDTO)
        {
            var response = new ServiceResponses<string>();
            try
            {
                var result = await userInterface.loginUser(loginDTO);
                return Ok(result);
            }
            catch(Exception e)
            {
                response.data="0";
                response.message = "Check User Controller Login API " + e.Message;
                response.status = false;

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                Console.WriteLine("API work Successfully");
            }
        }
    }
}
