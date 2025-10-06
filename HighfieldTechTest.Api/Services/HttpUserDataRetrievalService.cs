using HighfieldTechTest.Api.Models;
using System.Text.Json;

namespace HighfieldTechTest.Api.Services
{
    public class HttpUserDataRetrievalService : IUserDataRetrievalService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpUserDataRetrievalService> _logger;

        public HttpUserDataRetrievalService(IHttpClientFactory httpClientFactory, ILogger<HttpUserDataRetrievalService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var client = _httpClientFactory.CreateClient("HighfieldUsers");
            var response = await client.GetAsync("api/test");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to get user: {Status}", response.StatusCode);
                return new List<User>(); 
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            using var stream = await response.Content.ReadAsStreamAsync();
            var users = await JsonSerializer.DeserializeAsync<List<User>>(stream, options)
                        ?? new List<User>();
            return users;
        }

    }
}
