using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.WatchList;
using Microsoft.AspNetCore.Mvc;
using static CinemaApp.GCommon.OutputMessages.Watchlist;

namespace CinemaApp.Web.Controllers
{
    public class WatchlistController : BaseController
    {
        private readonly IWatchlistService watchlistService;
        private readonly ILogger<WatchlistController> logger;

        public WatchlistController(
            IWatchlistService watchlistService,
            ILogger<WatchlistController> logger)
        {
            this.watchlistService = watchlistService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = GetUserId()!;

            IEnumerable<WatchListMovieViewModel> watchListMovieViewModels =
                await watchlistService.GetUserWatchListAsync(userId);

            return View(watchListMovieViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Add([FromRoute(Name = "id")] Guid movieId)
        {
            string userId = GetUserId()!;

            try
            {
                await watchlistService.AddMovieToUserWatchlistAsync(userId, movieId);
            }
            catch (EntityAlreadyExistsException eaee)
            {
                logger.LogError(eaee,
                    string.Format(MovieAlreadyInWatchlistMessage, movieId, userId));

                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromRoute(Name = "id")] Guid movieId)
        {
            string userId = GetUserId()!;
            try
            {
                await watchlistService.RemoveMovieFromUserWatchlistAsync(userId, movieId);
            }
            catch (EntityNotFoundException enfe)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}