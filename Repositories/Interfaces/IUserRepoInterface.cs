using instantBid.DTOs;
using instantBid.Models;

namespace instantBid.Repositories.Interfaces
{
    public interface IUserRepoInterface
    {
        Task<User> addUser(User user);
        Task<IEnumerable<User>> getUsers(RegistrationDTO userDTO);
        Task<User> getUserByName(User user);
        Task <User> getUserByID(int id);
        Task<User> getUserByEmail(string email);
        Task updateUserData(User user);
        Task<List<User>> userProfile();
        
    }
}
