using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CinemaApp.GCommon.OutputMessages.Movie;
using static CinemaApp.GCommon.ApplicationConstants;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieService movieService;
        private readonly ILogger<MovieController> logger;
        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            this.movieService = movieService;
            this.logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllMoviesIndexViewModel> allMoviesViewModel = await movieService
                .GetAllMoviesOrderedByTitleAsync();
            return View(allMoviesViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieFormModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return View(formModel);
            }

            try
            {
                await movieService.CreateMovieAsync(formModel);

            }
            catch (EntityCreatePersistFailException ecpfe)
            {
                logger.LogError(ecpfe, CreateMovieFailureMessage);
                ModelState.AddModelError(string.Empty, CreateMovieFailureMessage);

                return View(formModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, UnexpectedErrorMessage);
                ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);

                return View(formModel);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            MovieDetailsViewModel? movieDetails = await movieService
                .GetMovieDetailsByIdAsync(id);
            if (movieDetails == null)
            {
                return NotFound();
            }
            return View(movieDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            MovieFormModel? movieFormModel = await movieService
                .GetMovieFormModelByIdAsync(id);
            if (movieFormModel == null)
            {
                return NotFound();
            }

            return View(movieFormModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, MovieFormModel formModel)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(); 
            }

            if (!ModelState.IsValid)
            {
                return View(formModel);
                
            }

            try
            {
                await movieService.EditMovieAsync(id, formModel);
            }
            catch (EntityNotFoundException enfe)
            {
                return NotFound();
            }
            catch (EntityCreatePersistFailException epfe)
            {
                logger.LogError(epfe, CreateMovieFailureMessage);
                return View(formModel);
            }
            return RedirectToAction(nameof(Details), new { id });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            { 
            return BadRequest();
            }

            MovieDetailsViewModel? movieDetails = await movieService
                .GetMovieDetailsByIdAsync(id);
            if (movieDetails == null)
            { 
             return NotFound();
            }
            return View(movieDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, MovieDetailsViewModel? movieDetailsViewModel)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            try
            {
                await movieService.SoftDeleteMovieAsync(id);
            }
            catch (EntityNotFoundException enfe)
            {
                return NotFound();
            }
            catch (EntityCreatePersistFailException epfe)
            {
                logger.LogError(epfe, CreateMovieFailureMessage);
                return View(movieDetailsViewModel);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
