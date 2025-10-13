using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using instantBid.Models;
using Microsoft.IdentityModel.Tokens;

namespace instantBid.HelperServices
{
    public class JWTTokenService
    {
        private readonly IConfiguration configuration;
        public JWTTokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //Generate JWT
        public string JWTServicesGenerator(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTToken:Key"]));
            var tokenHandelar = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Email", user.Email)
            };

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = configuration["JWTToken:Issuer"],
                Audience = configuration["JWTToken:Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandelar.CreateToken(tokenDescription);
            return tokenHandelar.WriteToken(token);
        }
    }
}