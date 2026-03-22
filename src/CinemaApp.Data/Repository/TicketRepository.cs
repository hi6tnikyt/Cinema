
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;

namespace CinemaApp.Data.Repository
{
    public class TicketRepository : BaseRepository, ITicketRepository
    {
        public TicketRepository(CinemaAppDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<bool> AddTicketAsync(Ticket ticket)
        {
            await DbContext!.Tickets.AddAsync(ticket);
            int result = await DbContext.SaveChangesAsync();

            return result == 1;
        }
    }
}
