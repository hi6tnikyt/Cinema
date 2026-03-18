
using CinemaApp.Services.Core.Contracts;
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
    }
}
