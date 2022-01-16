﻿using Admegatest.Configuration;
using Admegatest.Core.Models;
using Admegatest.Data.DbContexts;
using Admegatest.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Admegatest.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AdmegatestDbContext _context;
        private readonly JWTSettings _jwtsettings;

        public UserRepository(AdmegatestDbContext admegatestDBContext, IOptions<JWTSettings> jwtsettings)
        {
            _context = admegatestDBContext;
            _jwtsettings = jwtsettings.Value;
        }

        public async Task<User?> GetUserByTokenAsync(string token)
        {
            int userId = GetUserIdFromToken(token);

            if (userId > 0)
            {
                return await _context.Users
                    .Where(u => u.Id == Convert.ToInt32(userId)).FirstOrDefaultAsync();
            }

            return null;
        }

        private int GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtsettings.Key);

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

        public async Task<User?> LoginAsync(User user)
        {
            var userFound = await _context.Users
                .Where(u => u.Name == user.Name && u.Password == user.Password)
                .FirstOrDefaultAsync();

            if (userFound == null)
            {
                return null;
            }

            userFound.Token = GenerateAccessToken(userFound.Id);

            return userFound;
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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
