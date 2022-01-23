using AdMegasoft.Application.Configurations;
using AdMegasoft.Core.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdMegasoft.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTSettings _jwtsettings;

        public AccountService(IUserRepository userRepository, IOptions<JWTSettings> jwtsettings)
        {
            _userRepository = userRepository;
            _jwtsettings = jwtsettings.Value;
        }

        public void Login(string name, string password)
        {
            var userFound = _userRepository.GetUserByPasswordNameAsync(name, password);

            if (userFound == null)
            {
                // return response without token
            }

            //userFound.Token = GenerateAccessToken(userFound.Id);
            //return userFound;
        }
        private string GenerateAccessToken(int userId)
        {
            var tokenDescriptor = GetSecurityTokenDescriptor(userId);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor(int userId)
        {
            var key = Encoding.ASCII.GetBytes(_jwtsettings.Key);

            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, Convert.ToString(userId))
            });

            var oneDay = DateTime.UtcNow.AddDays(1);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = oneDay,
                SigningCredentials = signingCredentials,
            };

            return tokenDescriptor;
        }
    }
}
