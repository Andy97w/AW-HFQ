using HighfieldTechTest.Api.Models;
using HighfieldTechTest.Api.Services;

namespace HighfieldTechTest.Tests
{
    public class UserAgeServiceTests
    {
        [Fact]
        public async Task GetAgeUpdatedUsersAsync_ReturnsCorrectAges()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = "1", DateOfBirth = DateTime.Today.AddYears(-30) }
            };
            var service = new UserAgeService();

            // Act
            var result = await service.GetAgeUpdatedUsersAsync(users);

            // Assert
            Assert.Single(result);
            Assert.Equal("1", result[0].UserId);
            Assert.Equal(30, result[0].OriginalAge);
            Assert.Equal(50, result[0].AgePlusTwenty);
        }

        [Fact]
        public async Task GetAgeUpdatedUsersAsync_ReturnsEmptyList()
        {
            // Arrange
            var users = new List<User>();

            var service = new UserAgeService();

            // Act
            var result = await service.GetAgeUpdatedUsersAsync(users);

            // Assert
            Assert.Empty(result);
        }
    }
}
