
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
    }
}
