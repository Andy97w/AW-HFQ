using System.Text.Json.Serialization;

namespace HighfieldTechTest.Api.Models
{
    public class UserSummaryResponseDTO
    {
        public List<User> Users { get; set; } = new List<User>();

        [JsonPropertyName("ages")]
        public List<AgePlusTwentyDTO> AgePlusTwentyUsers { get; set; } = new List<AgePlusTwentyDTO>();

        [JsonPropertyName("topColours")]
        public List<TopColoursDTO> ColourCounts { get; set; } = new List<TopColoursDTO>();
    }
}
