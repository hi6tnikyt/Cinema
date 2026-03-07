
using System.Linq.Expressions;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class MovieRepository : IMovieRepository, IDisposable
    {
        private bool isDisposed = false;
        private readonly CinemaAppDbContext dbContext;
        public MovieRepository(CinemaAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateMovieAsync(Movie movie)
        {
           await dbContext.Movies.AddAsync(movie);
            int resultCount = await SaveChangesAsync();
            return resultCount > 0;
        }


        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await this.dbContext
                 .Movies
                 .AsNoTracking()
                 .OrderBy(m => m.Title)
                 .ToArrayAsync();
        }

  
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            isDisposed = true;
        }

        private async Task<int> SaveChangesAsync()
        {
           return await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesNoTrackingAsync(Expression<Func<Movie, Movie>>? projectFunc = null)
        {
            IQueryable<Movie> movieFetchQuery = this.dbContext
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

        public async Task<Movie?> GetMovieByIdAsync(Guid id)
        {
            return await this.dbContext
                 .Movies
                 .FindAsync(id);
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            return await dbContext
                .Movies.
                AnyAsync(m => m.Id == id);
        }

        public async Task<bool> EditMovieAsync(Movie movie)
        {
           dbContext.Movies.Update(movie);
            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task<bool> HardDeleteMovieAsync(Movie movie)
        {
            dbContext.Movies.Remove(movie);
            int resultCount = await dbContext.SaveChangesAsync();
            return resultCount == 1;
        }

        public async Task<bool> SoftDeleteMovieAsync(Movie movie)
        {
            movie.IsDeleted = true;
            dbContext.Movies.Update(movie);

            int resultCount = await SaveChangesAsync();
            return resultCount == 1;
        }
    }
}
