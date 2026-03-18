
using CinemaApp.Data.Models;

namespace CinemaApp.Services.Core.Contracts
{
    public interface ICinemaService
    {
        Task<IEnumerable<Cinema>> GetAllCinemaOrderByLocationAsync();
    }
}
