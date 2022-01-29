﻿using AdMegasoft.Application.Configurations;
using AdMegasoft.Application.Interfaces.Repositories;
using AdMegasoft.Application.Interfaces.Services;
using AdMegasoft.Application.Models;
using AdMegasoft.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdMegasoft.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTSettings _jwtsettings;

        public UserService(IUserRepository userRepository, IOptions<JWTSettings> jwtsettings)
        {
            _userRepository = userRepository;
            _jwtsettings = jwtsettings.Value;
        }

        public async Task<UserWithToken> LoginAsync(string name, string password)
        {
            var userFound = await _userRepository
                .GetActiveUserByPasswordNameAsync(name, password);

            return new UserWithToken
            {
                Name = name,
                Password = password,
                AccessToken = userFound == null ? null : GenerateAccessToken(userFound.Id)
            };
        }

        public async Task<User> GetUserFromAccessTokenAsync(string accessToken)
        {
            try
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
                var principle = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

                JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)securityToken;

                if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principle.FindFirst(ClaimTypes.Name)?.Value;

                    return await _userRepository.GetByIdAsync(Convert.ToInt32(userId)) ?? new User();
                }
            }
            catch (Exception)
            {
                return new User();
            }

            return new User();
        }

        private string GenerateAccessToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddDays(_jwtsettings.AccessTokenDurationInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
