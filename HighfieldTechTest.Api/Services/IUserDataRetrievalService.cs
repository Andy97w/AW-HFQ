using HighfieldTechTest.Api.Models;

namespace HighfieldTechTest.Api.Services
{
    public interface IUserDataRetrievalService
    {
        Task<List<User>> GetUsersAsync();
    }
}
