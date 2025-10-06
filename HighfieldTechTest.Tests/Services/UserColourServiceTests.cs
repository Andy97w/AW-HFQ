using HighfieldTechTest.Api.Models;
using HighfieldTechTest.Api.Services;

namespace HighfieldTechTest.Tests
{
    public class UserColourServiceTests
    {
        [Fact]
        public async Task GetColourCountsAsync_ReturnsCorrectCounts()
        {
            // Arrange
            var users = new List<User>
            {
                new User { FavouriteColour = "Red" },
                new User { FavouriteColour = "Blue" },
                new User { FavouriteColour = "Red" }
            };
            var service = new UserColourService();

            // Act
            var result = await service.GetColourCountsAsync(users);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Red", result[0].Colour);
            Assert.Equal(2, result[0].Count);
            Assert.Equal("Blue", result[1].Colour);
            Assert.Equal(1, result[1].Count);
        }

        [Fact]
        public async Task GetColourCountsAsync_EmptyUserList_ReturnsEmptyList()
        {
            // Arrange
            var users = new List<User>();
            var service = new UserColourService();

            // Act
            var result = await service.GetColourCountsAsync(users);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetColourCountsAsync_IsCaseInsensitive()
        {
            // Arrange
            var users = new List<User>
            {
                new User { FavouriteColour = "red" },
                new User { FavouriteColour = "Red" },
                new User { FavouriteColour = "RED" }
            };
            var service = new UserColourService();

            // Act
            var result = await service.GetColourCountsAsync(users);

            // Assert
            Assert.Single(result);
            Assert.Equal("red", result[0].Colour, ignoreCase: true);
            Assert.Equal(3, result[0].Count);
        }

        [Fact]
        public async Task GetColourCountsAsync_TrimsWhitespace()
        {
            // Arrange
            var users = new List<User>
            {
                new User { FavouriteColour = " Blue" },
                new User { FavouriteColour = "Blue " },
                new User { FavouriteColour = "Blue" }
            };
            var service = new UserColourService();

            // Act
            var result = await service.GetColourCountsAsync(users);

            // Assert
            Assert.Single(result);
            Assert.Equal("Blue", result[0].Colour, ignoreCase: true);
            Assert.Equal(3, result[0].Count);
        }
    }
}
