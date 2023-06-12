using CinemaManager.Data;
using CinemaManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly DBContext _dbContext;
        private readonly CancellationToken _cancellationToken;

        public MoviesRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
            _cancellationToken = new CancellationToken();
        }

        public async Task<IEnumerable<MoviesModel>> GetMoviesAsync()
        {
            try
            {
                return await _dbContext.Movies.ToListAsync(_cancellationToken);
            }
            catch (Exception) { throw; }
        }

        public async Task<MoviesModel> GetMovieByIdAsync(int id)
        {
            try
            {
                var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id, _cancellationToken);
                if (movie is null) { throw new ArgumentNullException(string.Empty); }

                return movie;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> CheckIfMovieIsAlreadyRegisteredAsync(MoviesModel moviesModel)
        {
            // Se o Id == 0 então o filme está sendo adicionando
            if (moviesModel.Id == 0)
            {
                foreach (var m in _dbContext.Movies)
                {
                    if (m.Title.ToUpper() == moviesModel.Title.ToUpper()) { return true; }
                }

                return false;
            }

            var movie = await GetMovieByIdAsync(moviesModel.Id);

            foreach (var m in _dbContext.Movies)
            {
                if (m.Title.ToUpper() == moviesModel.Title.ToUpper())
                {
                    if (m.Id != movie.Id) { return true; }
                }
            }

            return false;
        }

        public async Task<bool> CheckIfMovieIsLinkedToAnySessionAsync(int id)
        {
            try
            {
                var movie = await GetMovieByIdAsync(id);
                var sessions = await _dbContext.Sessions.ToListAsync(_cancellationToken);

                return sessions.Any(x => x.MovieTitle.ToUpper() == movie.Title.ToUpper());
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> AddMovieAsync(MoviesModel moviesModel)
        {
            try
            {
                moviesModel.CreatedAt = DateTime.Now;

                _dbContext.Movies.Add(moviesModel);
                await _dbContext.SaveChangesAsync(_cancellationToken);

                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> UpdateMovieAsync(MoviesModel moviesModel)
        {
            try
            {
                var movie = await GetMovieByIdAsync(moviesModel.Id);

                movie.Image = moviesModel.Image;
                movie.Title = moviesModel.Title;
                movie.Description = moviesModel.Description;
                movie.Duration = moviesModel.Duration;
                movie.UpdatedAt = DateTime.Now;

                _dbContext.Movies.Update(movie);
                await _dbContext.SaveChangesAsync(_cancellationToken);

                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> RemoveMovieAsync(int id)
        {
            try
            {
                var movie = await GetMovieByIdAsync(id);

                _dbContext.Movies.Remove(movie);
                await _dbContext.SaveChangesAsync(_cancellationToken);

                return true;
            }
            catch (Exception) { throw; }
        }
    }
}