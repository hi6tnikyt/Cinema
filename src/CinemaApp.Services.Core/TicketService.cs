
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Contracts;

namespace CinemaApp.Services.Core
{
    public class TicketService : ITicketService
    {
        private readonly IProjectionRepository projectionRepository;
        private readonly ITicketRepository ticketRepository;

        public TicketService(IProjectionRepository projectionRepository, ITicketRepository ticketRepository)
        {
            this.projectionRepository = projectionRepository;
            this.ticketRepository = ticketRepository;
        }

        public async Task<bool> BuyTicketAsync(Guid projectionId, string userId, int quantity)
        {
            Projection? projection = await projectionRepository
                .FindByIdAsync(projectionId);

            if (projection == null)
            {

                return false;
            }

            if (projection.AvailableTickets < quantity)
            {
                return false;
            }

            Ticket newTicket = new Ticket()
            {
                ProjectionId = projectionId,
                UserId = Guid.Parse(userId),
                Quantity = quantity
            };

            bool success =  await ticketRepository.AddTicketAsync(newTicket);

            if (success)
             {
                projection.AvailableTickets -= quantity;
               success &= await projectionRepository.EditProjectionAsync(projection);
            }
            return success;
        }
    }
}
