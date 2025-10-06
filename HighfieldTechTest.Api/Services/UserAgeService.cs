using HighfieldTechTest.Api.Extensions;
using HighfieldTechTest.Api.Models;

namespace HighfieldTechTest.Api.Services
{
    public class UserAgeService : IUserAgeService
    {
        public Task<List<AgePlusTwentyDTO>> GetAgeUpdatedUsersAsync(List<User> users)
        {
            var agePlusTwentyUsers = users.Select(user =>
            {
                var age = DateTimeExtensions.GetAge(user.DateOfBirth);

                return new AgePlusTwentyDTO
                {
                    UserId = user.Id,
                    OriginalAge = age,
                    AgePlusTwenty = age + 20
                };
            }).ToList();

            return Task.FromResult(agePlusTwentyUsers);
        }
    }
}
