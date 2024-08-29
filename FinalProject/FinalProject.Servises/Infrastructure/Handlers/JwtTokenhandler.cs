using FinalProject.BusinessLayer.Infrastructure.Handlers.Interfaces;
using FinalProject.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalProject.BusinessLayer.Infrastructure.Handlers
{
    public class JwtTokenhandler : IJwtTokenhandler
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        public JwtTokenhandler(IUserRepository userRepository, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
        }

    
        public async Task<string> CreateJWTTokenAsync(string username)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("JWTToken:SecretKey").Value);
            var user = await userRepository.GetUserByUsernameAsync(username);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenCreate = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(tokenCreate);
            return token;
        }


    }
}
