
using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.WatchList;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core
{
    public class WatchlistService : IWatchlistService
    {
        private readonly CinemaAppDbContext dbContext;

        public WatchlistService(CinemaAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<WatchListMovieViewModel>> GetUserWatchListAsync(string userId)
        {
            IEnumerable<WatchListMovieViewModel> watchListMovieViewModels =
                await dbContext.UsersMovies
                .Where(um => um.UserId == userId)
                .Select(um => new WatchListMovieViewModel()
                {
                    MovieId = um.Movie.Id,
                    Title = um.Movie.Title,
                    Genre = um.Movie.Genre.ToString(),
                    ReleaseDate = um.Movie!.ReleaseDate.ToString("dd/MM/yyyy"),
                    ImageUrl = um.Movie.ImageUrl
                })
                .ToArrayAsync();

            return watchListMovieViewModels;
        }
    }
}
