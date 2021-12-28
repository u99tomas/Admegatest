using Admegatest.Core.Models;
using Admegatest.Core.Models.AuthenticationAndAuthorization;
using Admegatest.Data;
using Admegatest.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;

namespace Admegatest.Services.Tests
{

    public class UserServiceTest
    {
        private readonly UserService _userService;
        private readonly AdmegatestDBContext _context;

        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<AdmegatestDBContext>()
                .UseInMemoryDatabase(databaseName: "Admegatest")
                .Options;

            _context = new AdmegatestDBContext(options);
            IOptions<JWTSettings> jwtSettings = Options.Create<JWTSettings>(new JWTSettings { SecretKey = "DvVMOjJOK7rRyTZ0DZTEXTnomrfVf4G1BdbEaTRY" });

            _userService = new UserService(_context, jwtSettings);
        }

        [Fact]
        public async void Login_ShouldReturnTheSameUserWithToken()
        {
            // Arrange
            var role = new Role { RoleId = 1, RoleDescription = "IsCustomer" };
            var user = new User { Name = "Juan", Password = "8d77353ef8af557af07fa7a80ff74c6b", Role = role, RoleId = role.RoleId, UserId = 1 };

            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            user.Password = "123";
            var userWithToken = await _userService.Login(user);

            // Assert
            Assert.NotNull(userWithToken);
            Assert.True(user.UserId == userWithToken.UserId);
        }
    }
}