
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class MovieRepository : IMovieRepository
    {
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

        public IQueryable<Movie> GetAllMoviesNoTracking()
        {
            return this.dbContext
                 .Movies
                 .AsNoTracking();
        }

        private async Task<int> SaveChangesAsync()
        {
           return await dbContext.SaveChangesAsync();
        }
    }
}
