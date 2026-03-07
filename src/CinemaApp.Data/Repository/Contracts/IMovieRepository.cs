
using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts
{
    public interface IMovieRepository
    {
        IQueryable<Movie> GetAllMoviesNoTracking();

        Task<IEnumerable<Movie>> GetAllMovies();

        Task<bool> CreateMovieAsync(Movie movie);

    }
}
