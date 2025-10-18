using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace instantBid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServiceInterface userService;
        public UsersController(IUserServiceInterface userService)
        {
            this.userService = userService;
        }



        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> getUserById()
        {
            var userId = int.Parse(User.FindFirst("userId").Value ?? "0");
            var result = await userService.getUserByID(userId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> getAllUser([FromQuery] RegistrationDTO userDTO)
        {
            var response = new ServiceResponses<string>();
            try
            {
                var result = await userService.getUsers(userDTO);
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

        //[Authorize]
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<ActionResult> RegisterUser([FromForm] RegistrationDTO registrationDTO)
        {
            try
            {
                var result = await userService.registerUser(registrationDTO);
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
            var result = await userService.loginUser(loginDTO);
            return Ok(result);
        }

        [HttpPatch]
        [Route("/updateUserData/{id}")]
        public async Task<IActionResult> updateUserProfile([FromForm] ProfileDTO profileDTO, int id)
        {
            Console.WriteLine("Controller hit with file: " + profileDTO.ProfileImage?.FileName);

            try
            {
                var result = await userService.updateProfile(profileDTO, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Check user Controller ........ " + ex);
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}
