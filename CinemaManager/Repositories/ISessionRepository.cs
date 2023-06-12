using CinemaManager.Models;

namespace CinemaManager.Repositories
{
    public interface ISessionRepository
    {
        Task<IEnumerable<SessionModel>> GetSessionsAsync();
        Task<SessionModel> GetSessionByIdAsync(int id);
        bool CheckIfSessionStartDateIsValid(SessionModel sessionModel);
        bool CheckIfMovieExists(string title);
        bool CheckIfTheaterExists(string title);
        Task<bool> CheckIfSessionIsAlreadyAllocatedAsync(SessionModel sessionModel);
        Task<bool> CheckIfSessionCanBeRemovedAsync(int id);
        Task<bool> AddSessionAsync(SessionModel sessionModel);
        Task<bool> RemoveSessionAsync(int id);
    }
}