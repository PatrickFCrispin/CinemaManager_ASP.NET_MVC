using CinemaManager.Converters;
using CinemaManager.Data;
using CinemaManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly DBContext _dbContext;
        private readonly CancellationToken _cancellationToken;

        private const string Animation3D = "3D";
        private const string Animation2D = "2D";
        private const string OriginalAudio = "Original";
        private const string DubbedAudio = "Dublado";

        public SessionRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _cancellationToken = new CancellationToken();
        }

        public async Task<IEnumerable<SessionModel>> GetSessionsAsync()
        {
            try
            {
                return await _dbContext.Sessions.ToListAsync(_cancellationToken);
            }
            catch (Exception) { throw; }
        }

        public async Task<SessionModel> GetSessionByIdAsync(int id)
        {
            try
            {
                var session = await _dbContext.Sessions.FirstOrDefaultAsync(x => x.Id == id, _cancellationToken);
                if (session is null) { throw new ArgumentNullException(string.Empty); }

                return session;
            }
            catch (Exception) { throw; }
        }

        public bool CheckIfSessionStartDateIsValid(SessionModel sessionModel)
        {
            return sessionModel.StartTime >= sessionModel.Date;
        }

        public bool CheckIfMovieExists(string title)
        {
            try
            {
                return _dbContext.Movies.Any(x => x.Title.ToUpper() == title.ToUpper());
            }
            catch (Exception) { throw; }
        }

        public bool CheckIfTheaterExists(string name)
        {
            try
            {
                return _dbContext.Theaters.Any(x => x.Name.ToUpper() == name.ToUpper());
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> CheckIfSessionIsAlreadyAllocatedAsync(SessionModel sessionModel)
        {
            try
            {
                var sessions = await GetSessionsAsync();

                foreach (var session in sessions)
                {
                    if (session.Id == sessionModel.Id)
                    {
                        if (session.Date == sessionModel.Date &&
                            (session.StartTime >= sessionModel.StartTime && session.StartTime <= sessionModel.EndTime))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> CheckIfSessionCanBeRemovedAsync(int id)
        {
            try
            {
                var session = await GetSessionByIdAsync(id);

                if (session.Date < DateTime.Now)
                {
                    if (session.EndTime < DateTime.Now) { return true; }
                }

                if (session.Date <= DateTime.Now.AddDays(10)) { return false; }

                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> AddSessionAsync(SessionModel sessionModel)
        {
            try
            {
                var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Title.ToUpper() == sessionModel.MovieTitle.ToUpper(), _cancellationToken);
                var theater = await _dbContext.Theaters.FirstOrDefaultAsync(x => x.Name.ToUpper() == sessionModel.TheaterName.ToUpper(), _cancellationToken);

                if (movie is null || theater is null) { return false; }

                sessionModel.MovieTitle = movie.Title;
                sessionModel.TheaterName = theater.Name;

                var endTime = SessionConverter.GetEndTimeFrom(movie.Duration);
                sessionModel.EndTime = sessionModel.StartTime.Add(endTime);

                switch (sessionModel.TypeOfAnimation)
                {
                    case Enums.TypeOfAnimationEnum.ThreeDimensional:
                        sessionModel.TypeOfAnimationDescription = Animation3D;
                        break;
                    case Enums.TypeOfAnimationEnum.TwoDimensional:
                        sessionModel.TypeOfAnimationDescription = Animation2D;
                        break;
                    default:
                        break;
                }

                switch (sessionModel.AudioType)
                {
                    case Enums.AudioTypeEnum.Original:
                        sessionModel.AudioTypeDescription = OriginalAudio;
                        break;
                    case Enums.AudioTypeEnum.Dubbed:
                        sessionModel.AudioTypeDescription = DubbedAudio;
                        break;
                    default:
                        break;
                }

                sessionModel.CreatedAt = DateTime.Now;

                _dbContext.Sessions.Add(sessionModel);
                await _dbContext.SaveChangesAsync(_cancellationToken);

                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> RemoveSessionAsync(int id)
        {
            try
            {
                var session = await GetSessionByIdAsync(id);

                _dbContext.Sessions.Remove(session);
                await _dbContext.SaveChangesAsync(_cancellationToken);

                return true;
            }
            catch (Exception) { throw; }
        }
    }
}