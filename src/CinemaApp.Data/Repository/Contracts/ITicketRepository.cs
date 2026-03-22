
using CinemaApp.Data.Models;

namespace CinemaApp.Data.Repository.Contracts
{
    public interface ITicketRepository
    {
        Task<bool> AddTicketAsync(Ticket ticket);
    }
}
