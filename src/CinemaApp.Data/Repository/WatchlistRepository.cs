using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class WatchlistRepository : BaseRepository, IWatchlistRepository
    {
        public WatchlistRepository(CinemaAppDbContext dbContext)
            : base(dbContext)
        { }

        public async Task<bool> AddUserMovieAsync(UserMovie userMovie)
        {
            await DbContext.UsersMovies.AddAsync(userMovie);
            int resultCount = await SaveChangeAsync();
            return resultCount == 1;
        }

        public async Task<bool> ExistsAsync(string userId, Guid movieId)
        {
            bool watchlistEntryExist = await DbContext
                .UsersMovies
                .AnyAsync(um => um.UserId == userId && um.MovieId == movieId);

            return watchlistEntryExist;
        }

        public async Task<IEnumerable<UserMovie>> GetAllUserMoviesAsync()
        {
            IEnumerable<UserMovie> userMovies = await DbContext
                .UsersMovies
                .AsNoTracking()
                .Include(um => um.Movie)
                .ToArrayAsync();

            return userMovies;
        }

        public async Task<UserMovie?> GetUserMovieAsync(string userId, Guid movieId)
        {
            UserMovie? userMovie = await DbContext
                .UsersMovies
                .SingleOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);

            return userMovie;
        }

        public async Task<UserMovie?> GetUserMovieIncludeDeletedAsync(string userId, Guid movieId)
        {
            UserMovie? userMovie = await DbContext
               .UsersMovies
               .IgnoreQueryFilters()
               .SingleOrDefaultAsync(um => um.UserId == userId && um.MovieId == movieId);
            return userMovie;
        }

        public Task<bool> HardDeleteUserMovieAsync(UserMovie userMovie)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDeleteUserMovieAsync(UserMovie userMovie)
        {
           userMovie.IsDeleted = true;
            DbContext.UsersMovies.Update(userMovie);
            int resultCount = await SaveChangeAsync();
            return resultCount == 1;
        }

        public async Task<bool> UpdateUserMovieAsync(UserMovie userMovie)
        {
            DbContext.UsersMovies.Update(userMovie);
            int resultCount = await SaveChangeAsync();
            return resultCount == 1;
        }
    }
}