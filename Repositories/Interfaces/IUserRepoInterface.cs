using instantBid.DTOs;
using instantBid.Models;

namespace instantBid.Repositories.Interfaces
{
    public interface IUserRepoInterface
    {
        Task<User> addUser(User user);
        Task<IEnumerable<User>> getUsers(RegistrationDTO userDTO);

        //Login Interface
        Task<User> getUserByEmail(string email);
    }
}
