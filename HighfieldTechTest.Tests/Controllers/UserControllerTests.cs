using HighfieldTechTest.Api.Controllers;
using HighfieldTechTest.Api.Models;
using HighfieldTechTest.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HighfieldTechTest.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<ILogger<UsersController>> _loggerMock;
        private readonly Mock<IUserDataRetrievalService> _userDataRetrievalServiceMock;
        private readonly Mock<IUserAgeService> _userAgeServiceMock;
        private readonly Mock<IUserColourService> _userColourServiceMock;
        private readonly UsersController _controller;

        public UserControllerTests()
        {
            _loggerMock = new Mock<ILogger<UsersController>>();
            _userDataRetrievalServiceMock = new Mock<IUserDataRetrievalService>();
            _userAgeServiceMock = new Mock<IUserAgeService>();
            _userColourServiceMock = new Mock<IUserColourService>();

            _controller = new UsersController(
                _loggerMock.Object,
                _userDataRetrievalServiceMock.Object,
                _userAgeServiceMock.Object,
                _userColourServiceMock.Object
            );
        }

        [Fact]
        public async Task GetSummary_ReturnsOk_WithValidData()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = "1", FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Today.AddYears(-30), FavouriteColour = "Red" }
            };
            var colourCounts = new List<TopColoursDTO>
            {
                new TopColoursDTO { Colour = "Red", Count = 1 }
            };
            var agePlusTwentyUsers = new List<AgePlusTwentyDTO>
            {
                new AgePlusTwentyDTO { UserId = "1", OriginalAge = 30, AgePlusTwenty = 50 }
            };

            _userDataRetrievalServiceMock.Setup(s => s.GetUsersAsync()).ReturnsAsync(users);
            _userColourServiceMock.Setup(s => s.GetColourCountsAsync(users)).ReturnsAsync(colourCounts);
            _userAgeServiceMock.Setup(s => s.GetAgeUpdatedUsersAsync(users)).ReturnsAsync(agePlusTwentyUsers);

            // Act
            var result = await _controller.GetSummary();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<UserSummaryResponseDTO>(okResult.Value);
            Assert.Equal(users, response.Users);
            Assert.Equal(colourCounts, response.ColourCounts);
            Assert.Equal(agePlusTwentyUsers, response.AgePlusTwentyUsers);
        }

        [Fact]
        public async Task GetSummary_ReturnsNotFound_WhenNoUsers()
        {
            // Arrange
            _userDataRetrievalServiceMock.Setup(s => s.GetUsersAsync()).ReturnsAsync(new List<User>());

            // Act
            var result = await _controller.GetSummary();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("No users found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetSummary_ReturnsNotFound_WhenUsersIsNull()
        {
            // Arrange
            _userDataRetrievalServiceMock.Setup(s => s.GetUsersAsync()).ReturnsAsync((List<User>)null);

            // Act
            var result = await _controller.GetSummary();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("No users found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetSummary_ReturnsServerError_OnException()
        {
            // Arrange
            _userDataRetrievalServiceMock.Setup(s => s.GetUsersAsync()).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.GetSummary();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("An error occurred while processing your request.", objectResult.Value);
        }
    }
}
