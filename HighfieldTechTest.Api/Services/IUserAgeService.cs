using HighfieldTechTest.Api.Models;

namespace HighfieldTechTest.Api.Services
{
    public interface IUserAgeService
    {
        Task<List<AgePlusTwentyDTO>> GetAgeUpdatedUsersAsync(List<User> users);
    }
}
