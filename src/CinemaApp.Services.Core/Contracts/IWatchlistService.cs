
using CinemaApp.Web.ViewModels.WatchList;

namespace CinemaApp.Services.Core.Contracts
{
    public interface IWatchlistService
    {
        Task<IEnumerable<WatchListMovieViewModel>> GetUserWatchListAsync(string userId);

        Task<bool> MovieIsInUserWatchlistAsync(string userId, Guid movieId);

        Task AddMovieToUserWatchlistAsync(string userId, Guid movieId);

        Task RemoveMovieFromUserWatchlistAsync(string userId, Guid movieId);
    }
}
