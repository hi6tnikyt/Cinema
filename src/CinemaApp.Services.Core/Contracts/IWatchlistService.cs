
using CinemaApp.Web.ViewModels.WatchList;

namespace CinemaApp.Services.Core.Contracts
{
    public interface IWatchlistService
    {
        Task<IEnumerable<WatchListMovieViewModel>> GetUserWatchListAsync(string userId);
    }
}
