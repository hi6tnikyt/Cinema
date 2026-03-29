using System.Net;
using CinemaApp.Services.Core;
using CinemaApp.Services.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [EnableCors("AllowMvcDomain")]
    public class MovieApiController : ControllerBase
    {
        private readonly IProjectionService projectionService;
        public MovieApiController(IProjectionService projectionService)
        {
            this.projectionService = projectionService;
        }
        [HttpGet("GetShowTimes")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<string>>> GetShowTimes(Guid movieId, Guid cinemaId)
        {
            IEnumerable<DateTime> showTimes = await projectionService
                 .GetProjectionShowtimesAsync(movieId, cinemaId);
                 IEnumerable<string> showTimesResult = showTimes
                .Select(st => st.ToString("g")) 
                .ToArray();

            return Ok(showTimesResult);
        }
    }
}
