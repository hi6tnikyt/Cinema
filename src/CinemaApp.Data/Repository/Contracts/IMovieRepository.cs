
using System.Linq.Expressions;
using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllMoviesNoTrackingAsync(Expression<Func<Movie, Movie>>? projectFunc = null);

        Task<IEnumerable<Movie>> GetAllMovies();

        Task<bool> CreateMovieAsync(Movie movie);

    }
}
