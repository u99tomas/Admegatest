using Admegatest.Configuration.Account;
using Admegatest.Core.Models.Account;
using Admegatest.Data.DbContexts;
using Admegatest.Extensions;
using Admegatest.Services.Services.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;

namespace Admegatest.Services.Tests.Account
{
    public class UserServiceTest
    {
        private readonly UserService _userService;
        private readonly AdmegatestDbContext _context;

        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<AdmegatestDbContext>()
                .UseInMemoryDatabase(databaseName: "Admegatest")
                .Options;

            _context = new AdmegatestDbContext(options);
            IOptions<JWTSettings> jwtSettings = Options.Create<JWTSettings>(new JWTSettings { Key = "DvVMOjJOK7rRyTZ0DZTEXTnomrfVf4G1BdbEaTRY" });

            _userService = new UserService(_context, jwtSettings);
        }

        [Fact]
        public async void Login_ShouldReturnTheSameUserWithToken()
        {
            // Arrange
            var role = new Role { RoleId = 1, RoleDescription = "IsCustomer" };
            var userForDb = new User { Name = "Juan", Password = "123".ToMD5(), Role = role, RoleId = role.RoleId, UserId = 1 };
            var user = new User { Name = "Juan", Password = "123"};

            _context.Users.Add(userForDb);
            _context.SaveChanges();

            // Act
            var result = await _userService.Login(user);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.UserId == userForDb.UserId);
        }
    }
}
