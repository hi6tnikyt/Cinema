using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.WatchList;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService watchlistService;

        public WatchlistController(IWatchlistService watchlistService)
        {
            this.watchlistService = watchlistService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = GetUserId();

            IEnumerable<WatchListMovieViewModel> watchListMovieViewModels = await watchlistService
                .GetUserWatchListAsync(userId);

            return View(watchListMovieViewModels);
        }
    }
}