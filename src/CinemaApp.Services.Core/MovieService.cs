

using System.ComponentModel;
using System.Globalization;
using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.Movie;
using Microsoft.EntityFrameworkCore;
using static CinemaApp.GCommon.ApplicationConstants;

namespace CinemaApp.Services.Core
{
    public class MovieService : IMovieService
    {
       private readonly IMovieRepository movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task CreateMovieAsync(MovieFormModel formModel)
        {
            Movie newMovie = new Movie()
            {
                Title = formModel.Title,
                Genre = formModel.Genre,
                ReleaseDate = DateOnly.FromDateTime(formModel.ReleaseDate),
                Description = formModel.Description,
                Director = formModel.Director,
                ImageUrl = formModel.ImageUrl,
                Duration = formModel.Duration
            };
         bool successAdd =  await movieRepository.CreateMovieAsync(newMovie);
            if (!successAdd)
            {
                throw new EntityCreatePersistFailException();
            }
        }

        public async Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync()
        {
           // Fetch data from database
            IEnumerable<Movie> allMoviesDb = await this.movieRepository
                .GetAllMoviesNoTrackingAsync(m => new Movie()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre,
                    ReleaseDate = m.ReleaseDate,
                    Director = m.Director,
                    ImageUrl = m.ImageUrl
                });

            // Process data (mapping)
            IEnumerable<AllMoviesIndexViewModel> allMoviesViewModel = allMoviesDb
                .Select(m => new AllMoviesIndexViewModel()
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = m.Genre.ToString(),
                    ReleaseDate = m.ReleaseDate.ToString(DefaultDateFormat, CultureInfo.InvariantCulture),
                    Director = m.Director,
                    ImageUrl = m.ImageUrl ?? DefaultImageUrl
                })
                .OrderBy(m => m.Title)
                .ThenBy(m => m.Genre)
                .ThenBy(m => m.Director)
                .ToArray();

            // return data
            return allMoviesViewModel;
        }
    }
}
