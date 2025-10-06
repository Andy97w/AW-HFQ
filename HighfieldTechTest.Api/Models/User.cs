using System.Text.Json.Serialization;

namespace HighfieldTechTest.Api.Models
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [JsonPropertyName("dob")]
        public DateTime DateOfBirth { get; set; }
        public string FavouriteColour { get; set; } = string.Empty;
    }
}
