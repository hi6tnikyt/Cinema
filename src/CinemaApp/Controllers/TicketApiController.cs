using System.Security.Claims;
using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Route("BuyTicket")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> BuyTicket(BuyTicketInputModel inputModel)
        {

          try
           {
            Guid? projectionId = await projectionService.GetProjectionIdByMovieCinemaAndShowtimeAsync(
                inputModel.MovieId,
                inputModel.CinemaId,
                inputModel.Showtime);
            if (projectionId == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                bool  isBought = await ticketService.BuyTicketAsync(projectionId.Value, userId, inputModel.Quantity);
                if (!isBought)
                {
                    return BadRequest();
                }
                return Ok();

            }
            catch (EntityInputDataException)
            {
                return BadRequest();
            }
        }
    }
}
