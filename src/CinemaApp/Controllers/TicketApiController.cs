using System.Security.Claims;
using System.Globalization;
using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TicketApiController : ControllerBase
    {
        private readonly IProjectionService projectionService;
        private readonly ITicketService ticketService;

        public TicketApiController(IProjectionService projectionService, ITicketService ticketService)
        {
            this.projectionService = projectionService;
            this.ticketService = ticketService;
        }

        [HttpPost("BuyTicket")]
        [ValidateAntiForgeryToken]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> BuyTicket([FromBody] BuyTicketInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!DateTime.TryParse(inputModel.Showtime, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedShowtime))
                {
                    return BadRequest(new { message = "Invalid date format provided." });
                }

                Guid? projectionId = await projectionService.GetProjectionIdByMovieCinemaAndShowtimeAsync(
                    inputModel.MovieId,
                    inputModel.CinemaId,
                    parsedShowtime);

                if (projectionId == null)
                {
                    return NotFound(new { message = "Projection not found for the selected time." });
                }

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                bool isBought = await ticketService.BuyTicketAsync(projectionId.Value, userId, inputModel.Quantity);

                if (!isBought)
                {
                    return BadRequest(new { message = "Purchase failed. Possible reasons: no available seats." });
                }

                return Ok(new { message = "Success!" });
            }
            catch (EntityInputDataException)
            {
                return BadRequest(new { message = "Invalid input data provided." });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }
    }
}