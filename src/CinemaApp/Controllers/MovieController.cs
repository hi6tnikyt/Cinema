using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CinemaApp.GCommon.OutputMessages.Movie;
using static CinemaApp.GCommon.ApplicationConstants;

namespace CinemaApp.Web.Controllers
{
    [AllowAnonymous]
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
        public async Task<IActionResult> Index(MovieAllIndexViewModel movieAllInputModel)
        {
            string? userId = GetUserId();

            int pageSize = 3;

            int currentPage = movieAllInputModel.PageNumber > 0 ? movieAllInputModel.PageNumber : 1;

            var pagedMovies = await movieService
                .GetAllMoviesOrderedByTitleAsync(userId, movieAllInputModel.SearchQuery, currentPage, pageSize);

            int totalMoviesCount = await movieService.GetMoviesCountAsync(movieAllInputModel.SearchQuery);

            int totalPages = (int)Math.Ceiling(totalMoviesCount / (double)pageSize);

            MovieAllIndexViewModel movieAllVm = new MovieAllIndexViewModel()
            {
                SearchQuery = movieAllInputModel.SearchQuery,
                PageNumber = currentPage,
                TotalPages = totalPages,
                Movies = pagedMovies.ToList()
            };

            return View(movieAllVm);
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
                TempData[ErrorTemDataKey] = CreateMovieFailureMessage;


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, UnexpectedErrorMessage);
                TempData[ErrorTemDataKey] = UnexpectedErrorMessage;

                return RedirectToAction(nameof(Index));
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

            TempData[SuccessTempDataKey] = "Edited succes!";
            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("movies/get-details/{id}")]
        public async Task<IActionResult> DetailsPartial(Guid id)
        {
            MovieDetailsViewModel? movieDetails = await movieService
                .GetMovieDetailsByIdAsync(id);

            if (movieDetails == null)
            {
                return NotFound();
            }

            return PartialView("~/Views/Shared/_MovieDetailsPartial.cshtml", movieDetails);
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
