
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Cinema;

namespace CinemaApp.Services.Core.Contracts
{
    public interface ICinemaService
    {
        Task<IEnumerable<Cinema>> GetAllCinemaOrderByLocationAsync();

        Task<CinemaProgramViewModel?> GetCinemaProgramByIdAsync(Guid cinemaId);
    }
}
