using Application.Configurations;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Requests;
using Application.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly JWTSettings _jwtsettings;
        private readonly IUnitOfWork<int> _unitOfWork;

        public UserService(IUnitOfWork<int> unitOfWork, IOptions<JWTSettings> jwtsettings)
        {
            _jwtsettings = jwtsettings.Value;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponse?> LoginAsync(TokenRequest tokenRequest)
        {
            var userResponse = await GetUserResponseAsync(tokenRequest.Name, tokenRequest.Password);

            if (userResponse == null) return null; // AVOID NULL REFERENCE

            userResponse.AccessToken = GenerateAccessToken(userResponse.Id);

            userResponse.Permissions = await GetPermissionByUserIdAsync(userResponse.Id);

            return userResponse;
        }

        public async Task<UserResponse?> GetUserFromAccessTokenAsync(string accessToken)
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
                    var userIdAsString = principle.FindFirst(ClaimTypes.Name)?.Value;
                    var userIdAsInt = Convert.ToInt32(userIdAsString);

                    var userResponse = await _unitOfWork.Repository<User>()
                        .Entities
                        .Select(u => new UserResponse { Id = u.Id, Name = u.Name })
                        .Where(u => u.Id == userIdAsInt)
                        .FirstOrDefaultAsync();

                    if (userResponse == null)
                    {
                        return null; // AVOID NULL REFERENCE
                    }

                    var permissions = await GetPermissionByUserIdAsync(userResponse.Id);
                    userResponse.Permissions = permissions;

                    return userResponse;
                }
            }
            catch (Exception)
            { }

            return null;
        }

        private async Task<UserResponse?> GetUserResponseAsync(string name, string password)
        {
            var userResponse = await _unitOfWork.Repository<User>()
                .Entities
                .Where(u => u.Name == name && u.Password == password && u.IsActive)
                .Select(u => new UserResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    Password = u.Password,
                })
                .FirstOrDefaultAsync();

            return userResponse;
        }

        private async Task<List<PermissionResponse>> GetPermissionByUserIdAsync(int userId)
        {
            var sql = @"SELECT P.Description, P.Name FROM UserRoles UR
                        JOIN RolePermissions RP
                            ON UR.RoleId = RP.RoleId
                        JOIN Permissions P
                            ON P.Id = RP.PermissionId
                        WHERE UserId = {0}";

            var permissionsResponse = await _unitOfWork.Repository<Permission>()
                .FromSqlRaw(sql, userId)
                .Select(p => new PermissionResponse
                {
                    Name = p.Name,
                    Description = p.Description,
                })
                .ToListAsync();

            return permissionsResponse;
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
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
