using HighfieldTechTest.Api.Models;

namespace HighfieldTechTest.Api.Services
{
    public class UserColourService : IUserColourService
    {
        public Task<List<TopColoursDTO>> GetColourCountsAsync(List<User> users)
        {
            //Ensure we only process users with a valid colour
            var usersWithColours = users
                .Where(u => !string.IsNullOrWhiteSpace(u.FavouriteColour))
                .ToList();

            if (usersWithColours.Count != 0)
            {
                var stats = usersWithColours
                    .GroupBy(u => u.FavouriteColour!.Trim(), StringComparer.OrdinalIgnoreCase)
                    .Select(g => new TopColoursDTO
                    {
                        Colour = g.Key,
                        Count = g.Count()
                    })
                    .OrderByDescending(c => c.Count)
                    .ThenBy(c => c.Colour, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                return Task.FromResult(stats);
            }

            return Task.FromResult(new List<TopColoursDTO>());
        }
    }
}
