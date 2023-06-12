using CinemaManager.Models;

namespace CinemaManager.Repositories
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<MoviesModel>> GetMoviesAsync();
        Task<MoviesModel> GetMovieByIdAsync(int id);
        Task<bool> CheckIfMovieIsAlreadyRegisteredAsync(MoviesModel moviesModel);
        Task<bool> CheckIfMovieIsLinkedToAnySessionAsync(int id);
        Task<bool> AddMovieAsync(MoviesModel moviesModel);
        Task<bool> UpdateMovieAsync(MoviesModel moviesModel);
        Task<bool> RemoveMovieAsync(int id);
    }
}