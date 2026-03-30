
namespace CinemaApp.Services.Core.Contracts
{
    public interface IProjectionService
    {
        Task<IEnumerable<DateTime>> GetProjectionShowtimesAsync(Guid movieId, Guid cinemaId);

        Task<Guid?> GetProjectionIdByMovieCinemaAndShowtimeAsync(Guid movieId, Guid cinemaId, DateTime showtime);
    }
}
