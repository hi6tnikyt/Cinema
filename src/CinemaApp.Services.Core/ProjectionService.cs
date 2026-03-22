
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Contracts;

namespace CinemaApp.Services.Core
{
    public class ProjectionService : IProjectionService
    {
        private readonly IProjectionRepository _projectionRepository;
        public ProjectionService(IProjectionRepository projectionRepository)
        {
            this._projectionRepository = projectionRepository;
        } 

        public async Task<Guid?> GetProjectionIdByMovieCinemaAndShowtimeAsync(Guid movieId, Guid cinemaId, DateTime showtime)
        {
            TimeOnly timeToMatch = TimeOnly.FromDateTime(showtime);

            IEnumerable<Projection> projections = await _projectionRepository
                .GetProjectionsAsync(pr => pr.MovieId == movieId &&
                                           pr.CinemaId == cinemaId &&
                                           pr.Showtime == timeToMatch);

            Projection? projection = projections.SingleOrDefault();

            if (projection == null)
            {
                return null;
            }

            return projection.Id;
        }

        public async Task<IEnumerable<DateTime>> GetProjectionShowtimesAsync(Guid movieId, Guid cinemaId)
        {
            IEnumerable<Projection> projections = await _projectionRepository
                .GetProjectionsAsync(pr => pr.MovieId == movieId && pr.CinemaId == cinemaId && pr.AvailableTickets > 0);

            IEnumerable<DateTime> projectionShowtimes = projections
                .Select(pr => DateTime.Today.Add(pr.Showtime.ToTimeSpan()))
                .Distinct()
                .OrderBy(t => t)
                .ToArray();

            return projectionShowtimes;
        }
    }
}
