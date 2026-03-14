
using System.Linq.Expressions;
using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMoviesNoTrackingAsync(Expression<Func<Movie, Movie>>? projectFunc = null);

        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie?> GetMovieByIdAsync(Guid id);

        Task<bool> CreateMovieAsync(Movie movie);

        Task<bool> EditMovieAsync(Movie movie);

        Task<bool> ExistByIdAsync(Guid id);

        Task<bool> HardDeleteMovieAsync(Movie movie);

        Task<bool> SoftDeleteMovieAsync(Movie movie);
        Task<IEnumerable<Movie>> GetAllMoviesWithWatchlistAsync();
    }
}
