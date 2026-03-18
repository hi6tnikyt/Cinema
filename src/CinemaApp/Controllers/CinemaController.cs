
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Cinema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ICinemaService _cinemaService;

        public CinemaController(ICinemaService cinemaService)
        {
            this._cinemaService = cinemaService;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CinemaApp.Data.Models.Cinema> allCinemas = await _cinemaService
                .GetAllCinemaOrderByLocationAsync();

            return View(allCinemas);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Program([FromRoute(Name = "id")] Guid cinemaId)
        {
            CinemaProgramViewModel? model = await _cinemaService.GetCinemaProgramByIdAsync(cinemaId);

            if (model == null)
            {
                return RedirectToAction(nameof(Index)); // Или върни 404
            }

            return View(model);
        }
    }
}
