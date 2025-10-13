using Azure;
using BCrypt.Net;
using instantBid.DTOs;
using instantBid.HelperServices;
using instantBid.Model;
using instantBid.Models;
using instantBid.Repositories.Implementations;
using instantBid.Repositories.Interfaces;
using instantBid.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace instantBid.Services.Implementstions
{
    public class UserService : IUserServiceInterface
    {

        private readonly IUserRepoInterface userRepoInterface;
        private readonly JWTTokenService jWTTokenService;
        public UserService(IUserRepoInterface userRepoInterface, JWTTokenService jWTTokenService)
        {
            this.userRepoInterface = userRepoInterface;
            this.jWTTokenService = jWTTokenService;
        }

        public async Task<ServiceResponses<IEnumerable<User>>> getUsers(RegistrationDTO userDTO)
        {
            var response = new ServiceResponses<IEnumerable<User>>();

            var allUserResponse = await userRepoInterface.getUsers(userDTO);

            if (allUserResponse == null)
            {
                response.data = null;
                response.message = "User is null";
                response.status = false;

                return response;
            }

            response.data = allUserResponse;
            response.message = "User fetched successfully";
            response.status = true;

            return response;
        }

        public async Task<ServiceResponses<string>> loginUser(LoginDTO loginDTO)
        {
            var response = new ServiceResponses<string>();

            var existEmail = await userRepoInterface.getUserByEmail(loginDTO.Email);
            if(existEmail == null
                || string.IsNullOrWhiteSpace(existEmail.Password)
                || !existEmail.Password.StartsWith("$2")
                || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, existEmail.Password))
            {
                response.data = "0";
                response.message = "Invalid email or password.";
                response.status = false;
                return response;
            }

            response.data = jWTTokenService.JWTServicesGenerator(existEmail);
            response.message = "Login successfully";
            response.status = true;
            return response;
        }

        public async Task<ServiceResponses<string>> registerUser(RegistrationDTO registrationDTO)
        {

            var response = new ServiceResponses<string>();
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(registrationDTO.Password);
            var user = new User
            {
                Name = registrationDTO.Name,
                Email = registrationDTO.Email,
                Password = hashPassword,
                CreatedAt = registrationDTO.CreatedAt,
                IsActive = registrationDTO.IsActive,
                AccountBalance = registrationDTO.AccountBalance,
                Address = registrationDTO.Address,
                DateOfBirth = registrationDTO.DateOfBirth,
                ProfileImage = registrationDTO.ProfileImage,
                RoleId = 2,
            };

            await userRepoInterface.addUser(user);

            response.data = "1";
            response.message = "Register successful";
            response.status = true;

            return response;
        }

    }
}
