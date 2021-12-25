using Admegatest.Core.Models;
using Admegatest.Core.Models.AuthenticationAndAuthorization;
using Admegatest.Data.InterfacesForRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AdmegatestDBContext _admegatestDBContext;
        private readonly JWTSettings _jwtsettings;

        public UserRepository(AdmegatestDBContext admegatestDBContext, IOptions<JWTSettings> jwtsettings)
        {
            _admegatestDBContext = admegatestDBContext;
            _jwtsettings = jwtsettings.Value;
        }

        public Task<User> GetUserByAccessToken(string accessToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserWithToken?> Login(User user)
        {
            var userFound = await _admegatestDBContext.Users.Include(u => u.Role)
                .Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();

            if (userFound == null)
            {
                return null;
            }

            var refreshToken = GenerateRefreshToken();
            userFound.RefreshTokens.Add(refreshToken);
            await _admegatestDBContext.SaveChangesAsync();

            var userWithToken = new UserWithToken(userFound);
            userWithToken.RefreshToken = refreshToken.Token;
            userWithToken.AccessToken = GenerateAccessToken(user.UserId);

            return userWithToken;
        }

        private string GenerateAccessToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                refreshToken.Token = Convert.ToBase64String(randomNumber);
            }
            refreshToken.ExpiryDate = DateTime.UtcNow.AddMonths(1);

            return refreshToken;
        }
    }
}
