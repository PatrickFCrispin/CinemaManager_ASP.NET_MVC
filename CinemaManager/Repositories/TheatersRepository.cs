using CinemaManager.Data;
using CinemaManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager.Repositories
{
    public class TheatersRepository : ITheatersRepository
    {
        private readonly DBContext _dbContext;
        private readonly CancellationToken _cancellationToken;

        public TheatersRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _cancellationToken = new CancellationToken();
        }

        public async Task<IEnumerable<TheatersModel>> GetTheatersAsync()
        {
            try
            {
                return await _dbContext.Theaters.ToListAsync(_cancellationToken);
            }
            catch (Exception) { throw; }
        }
    }
}