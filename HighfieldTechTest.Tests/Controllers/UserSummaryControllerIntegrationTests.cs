using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using HighfieldTechTest.Api;
using HighfieldTechTest.Api.Models;

namespace HighfieldTechTest.Tests.Controllers
{
    public class UserSummaryControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UserSummaryControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetSummary_ReturnsOkResponse()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/users/summary");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetSummary_ReturnsValidResponseWhenPostingResultToHighfieldAPI()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act: Call API
            var response = await client.GetAsync("/api/users/summary");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Deserialize the response to DTO
            var result = await response.Content.ReadFromJsonAsync<UserSummaryResponseDTO>();
            Assert.NotNull(result);

            // Act: Post to the Highfield API
            using var httpClient = new HttpClient();
            var postResponse = await httpClient.PostAsJsonAsync("https://recruitment.highfieldqualifications.com/api/test", result);

            // Assert: Should return 200 if correct
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        }
    }
}
