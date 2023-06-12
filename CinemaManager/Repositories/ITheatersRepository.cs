using CinemaManager.Models;

namespace CinemaManager.Repositories
{
    public interface ITheatersRepository
    {
        Task<IEnumerable<TheatersModel>> GetTheatersAsync();
    }
}