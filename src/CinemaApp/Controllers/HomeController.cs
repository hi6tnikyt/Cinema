namespace CinemaApp.Web.Controllers
{
    using System.Diagnostics;
    using CinemaApp.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    [AllowAnonymous]
    public class HomeController : BaseController
    {

        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StatusCodeError(int code) 
        {
            if (code == StatusCodes.Status400BadRequest)
            {
                return View("BadRequest"); 
            }

            if(code == StatusCodes.Status404NotFound)
            {
                return View("NotFound");
            }

            if (code == StatusCodes.Status500InternalServerError)
            {
                return View("ServerError");
            }

            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
