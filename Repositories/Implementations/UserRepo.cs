using instantBid.DBContext;
using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Model;
using instantBid.Models;
using instantBid.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace instantBid.Repositories.Implementations
{
    public class UserRepo : IUserRepoInterface
    {
        private readonly AppDbContext dbContext;
        public UserRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> addUser(User user)
        {
            try
            {
                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> getUserByEmail(string email)
        {
            var existEmail = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return (existEmail);
        }

        public async Task<IEnumerable<User>> getUsers(RegistrationDTO userDTO)
        {
           return await dbContext.Users
                .Select(u => new User
           {
               Name = u.Name,
               Email = u.Email,
               DateOfBirth = u.DateOfBirth,
               Address = u.Address,
               ProfileImage = u.ProfileImage,
               AccountBalance   = u.AccountBalance,
               IsActive     = u.IsActive,
               CreatedAt = u.CreatedAt,
               Role = u.Role
           }).ToListAsync();
        }
    }
}
