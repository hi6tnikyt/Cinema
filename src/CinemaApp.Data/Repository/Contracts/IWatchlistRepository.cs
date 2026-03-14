

using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts
{
    public interface IWatchlistRepository
    {
        Task<IEnumerable<UserMovie>> GetAllUserMoviesAsync();

        Task<UserMovie?> GetUserMovieIncludeDeletedAsync(string userId, Guid movieId);

        Task<UserMovie?> GetUserMovieAsync(string userId, Guid movieId);

        Task<bool> ExistsAsync(string userId, Guid movieId);

        Task<bool> AddUserMovieAsync(UserMovie userMovie);

        Task<bool> SoftDeleteUserMovieAsync(UserMovie userMovie);

        Task<bool> HardDeleteUserMovieAsync(UserMovie userMovie);

        Task<bool> UpdateUserMovieAsync(UserMovie userMovie);
    }
}
