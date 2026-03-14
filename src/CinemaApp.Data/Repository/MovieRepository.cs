
using System.Linq.Expressions;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {

        public MovieRepository(CinemaAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<bool> CreateMovieAsync(Movie movie)
        {
           await DbContext.Movies.AddAsync(movie);
            int resultCount = await SaveChangesAsync();
            return resultCount > 0;
        }


        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await this.DbContext
                 .Movies
                 .AsNoTracking()
                 .OrderBy(m => m.Title)
                 .ToArrayAsync();
        }


        private async Task<int> SaveChangesAsync()
        {
           return await DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesNoTrackingAsync(Expression<Func<Movie, Movie>>? projectFunc = null)
        {
            IQueryable<Movie> movieFetchQuery = this.DbContext
                 .Movies
                 .AsNoTracking()
                 .OrderBy(m => m.Title);
            if (projectFunc != null)
            {
                movieFetchQuery = movieFetchQuery
                    .Select(projectFunc);
            }
            return await movieFetchQuery.ToArrayAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesWithWatchlistAsync()
        {
            return await this.DbContext.Movies
                .Include(m => m.MovieUsersWatchlist) 
                .Where(m => m.IsDeleted == false)    
                .ToListAsync();
        }

        public async Task<Movie?> GetMovieByIdAsync(Guid id)
        {
            return await this.DbContext
                 .Movies
                 .FindAsync(id);
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await DbContext
                .Movies.
                AnyAsync(m => m.Id == id);
        }

        public async Task<bool> EditMovieAsync(Movie movie)
        {
            DbContext.Movies.Update(movie);
            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task<bool> HardDeleteMovieAsync(Movie movie)
        {
            DbContext.Movies.Remove(movie);
            int resultCount = await DbContext.SaveChangesAsync();
            return resultCount == 1;
        }

        public async Task<bool> SoftDeleteMovieAsync(Movie movie)
        {
            movie.IsDeleted = true;
            DbContext.Movies.Update(movie);

            int resultCount = await SaveChangesAsync();
            return resultCount == 1;
        }
    }
}
