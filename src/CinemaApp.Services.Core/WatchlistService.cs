using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.GCommon.Exceptions;
using CinemaApp.Services.Core.Contracts;
using CinemaApp.Web.ViewModels.WatchList;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Services.Core
{
    public class WatchlistService : IWatchlistService
    {
        private readonly CinemaAppDbContext dbContext;
        private readonly IMovieRepository movieRepository;
        private readonly IWatchlistRepository watchlistRepository;

        public WatchlistService(
            CinemaAppDbContext dbContext,
            IMovieRepository movieRepository,
            IWatchlistRepository watchlistRepository)
        {
            this.dbContext = dbContext;
            this.movieRepository = movieRepository;
            this.watchlistRepository = watchlistRepository;
        }

        public async Task AddMovieToUserWatchlistAsync(string userId, Guid movieId)
        {
            bool userWatchlistEntryExist = await watchlistRepository
                .ExistsAsync(userId, movieId);

            if (userWatchlistEntryExist)
            {
                throw new EntityAlreadyExistsException();
            }

            bool movieExist = await movieRepository
                .ExistByIdAsync(movieId);

            if (!movieExist)
            {
                throw new EntityNotFoundException();
            }

            UserMovie newUserMovie = new UserMovie()
            {
                UserId = userId,
                MovieId = movieId
            };

            bool successAdd = await watchlistRepository
                .AddUserMovieAsync(newUserMovie);

            if (!successAdd)
            {
                throw new EntityCreatePersistFailException();
            }
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

        public async Task<bool> MovieIsInUserWatchlistAsync(string userId, Guid movieId)
        {
            return await dbContext.UsersMovies
                .AnyAsync(um => um.UserId == userId && um.MovieId == movieId);
        }
    }
}