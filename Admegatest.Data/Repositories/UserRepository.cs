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

        public async Task<UserWithToken?> Login(User user)
        {
            user.Password = GetEncryptedPassword(user.Password);
            var userFound = await _admegatestDBContext.Users.Include(u => u.Role)
                .Where(u => u.Name == user.Name && u.Password == user.Password).FirstOrDefaultAsync();

            if (userFound == null)
            {
                return null;
            }

            var userWithToken = new UserWithToken(userFound);
            userWithToken.Token = GenerateAccessToken(userFound.UserId);

            return userWithToken;
        }

        private string GetEncryptedPassword(string password)
        {
            var provider = MD5.Create();
            string salt = "S0m3R@nd0mSalt";
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
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
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, Convert.ToString(userId))
            });

            var oneDay = DateTime.UtcNow.AddDays(1);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = oneDay,
                SigningCredentials = signingCredentials,
            };

            return tokenDescriptor;
        }

        public async Task<User?> GetUserByToken(string token)
        {
            int userId = GetUserIdFromToken(token);

            if (userId > 0)
            {
                return await _admegatestDBContext.Users.Include(u => u.Role)
                    .Where(u => u.UserId == Convert.ToInt32(userId)).FirstOrDefaultAsync();
            }

            return null;
        }

        private int GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken securityToken;
            ClaimsPrincipal claimsPrincipal;

            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            }
            catch (Exception)
            {
                return -1;
            }

            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null)
            {
                return -1;
            }

            bool signatureAlgorithmIsValid = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);

            if (signatureAlgorithmIsValid)
            {
                var userId = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value ?? "-1";
                return Convert.ToInt32(userId);
            }

            return -1;
        }
    }
}
