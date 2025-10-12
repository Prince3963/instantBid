using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Models;

namespace instantBid.Services.Interfaces
{
    public interface IUserServiceInterface
    {
        Task<ServiceResponses<string>> registerUser(RegistrationDTO registrationDTO);
        Task<ServiceResponses<IEnumerable<User>>> getUsers(RegistrationDTO userDTO);
        Task<ServiceResponses<string>> loginUser(LoginDTO loginDTO);    
    }
}
