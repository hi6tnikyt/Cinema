
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core.Contracts;

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
    }
}
