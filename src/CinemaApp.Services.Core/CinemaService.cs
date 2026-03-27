
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Cinema;

namespace CinemaApp.Services.Core
{
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;

        public CinemaService(ICinemaRepository cinemaRepository)
        {
            this._cinemaRepository = cinemaRepository;  
        }
        public async Task<IEnumerable<Cinema>> GetAllCinemaOrderByLocationAsync()
        {
            IEnumerable<Cinema> allCinemas = (await _cinemaRepository
                 .GetAllCinemas(
                filterQuery: null,
                projectionQuery: c => new Cinema { 
                Id = c.Id,
                    Name = c.Name,
                    Location = c.Location
                }))
                    .OrderBy(c => c.Location)
                    .ToArray(); 

            return allCinemas;
        }

        public async Task<CinemaProgramViewModel> GetCinemaProgramByIdAsync(Guid cinemaId)
        {
            Cinema? cinema = await _cinemaRepository.GetCinemaByIdIncludeMovies(cinemaId);

            if (cinema == null)
            {
                return null;
            }

            return new CinemaProgramViewModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                Location = cinema.Location,
                ProjectionsMovies = cinema.Projections
                    .Select(p => new CinemaProgramMoviesViewModel
                    {
                        Id = p.Movie.Id,
                        Title = p.Movie.Title,
                        Director = p.Movie.Director,
                        ImageUrl = p.Movie.ImageUrl!
                    })
                    .ToList()
            };
        }
    }
}
