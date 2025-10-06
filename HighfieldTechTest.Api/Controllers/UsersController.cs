using HighfieldTechTest.Api.Models;
using HighfieldTechTest.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HighfieldTechTest.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserDataRetrievalService _userDataRetrievalService;
        private readonly IUserAgeService _userAgeService;
        private readonly IUserColourService _userColourService;

        public UsersController(ILogger<UsersController> logger, IUserDataRetrievalService userDataRetrievalService, IUserAgeService userAgeService, IUserColourService userColourService)
        {
            _logger = logger;
            _userDataRetrievalService = userDataRetrievalService;
            _userAgeService = userAgeService;
            _userColourService = userColourService;
        }

        /// <summary>
        /// Gets a summary of user data including original users, their ages (plus 20), and favourite colour stats.
        /// </summary>
        /// <remarks>
        /// Retrieves all users, calculates their current age and age plus twenty, and returns the most popular favourite colours.
        /// </remarks>
        /// <returns>
        /// A <see cref="UserSummaryResponseDTO"/> containing:
        /// <list type="bullet">
        /// <item><description>Users: The original user list.</description></item>
        /// <item><description>AgePlusTwentyUsers: Each user's age and their age plus twenty.</description></item>
        /// <item><description>ColourCounts: The most popular favourite colours and their counts.</description></item>
        /// </list>
        /// </returns>
        /// <response code="200">Returns the user summary response.</response>
        /// <response code="404">No users found.</response>
        /// <response code="500">An error occurred while processing the request.</response>
        [HttpGet("summary")]
        public async Task<ActionResult<UserSummaryResponseDTO>> GetSummary()
        {
            try
            {
                var users = await _userDataRetrievalService.GetUsersAsync();

                if (users == null || users.Count == 0)
                {
                    return NotFound("No users found.");
                }

                var colourCounts = await _userColourService.GetColourCountsAsync(users);

                var ageUpdatedUsers = await _userAgeService.GetAgeUpdatedUsersAsync(users);

                var summaryResponse = new UserSummaryResponseDTO
                {
                    Users = users,
                    ColourCounts = colourCounts,
                    AgePlusTwentyUsers = ageUpdatedUsers
                };

                return Ok(summaryResponse);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
