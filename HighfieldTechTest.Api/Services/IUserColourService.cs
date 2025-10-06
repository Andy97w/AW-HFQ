using HighfieldTechTest.Api.Models;

namespace HighfieldTechTest.Api.Services
{
    public interface IUserColourService
    {
        Task<List<TopColoursDTO>> GetColourCountsAsync(List<User> users);
    }
}
