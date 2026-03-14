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
            UserMovie? userMovie = await watchlistRepository
                .GetUserMovieIncludeDeletedAsync(userId, movieId);
            if (userMovie != null && userMovie.IsDeleted == false)
            { 
             throw new EntityAlreadyExistsException();
            }

            bool movieExist = await movieRepository.ExistByIdAsync(movieId);
            if (!movieExist)
            {
                throw new EntityNotFoundException();
            }

            if (userMovie == null)
            {
                UserMovie newUserMovie = new UserMovie()
                {
                    UserId = userId,
                    MovieId = movieId,
                    IsDeleted = false
                };

                bool successAdd = await watchlistRepository.AddUserMovieAsync(newUserMovie);
                if (!successAdd)
                {
                    throw new EntityCreatePersistFailException();
                }
            }
            else
            {
                userMovie.IsDeleted = false;
                bool successUpdate = await watchlistRepository
                    .UpdateUserMovieAsync(userMovie);
                if (!successUpdate)
                {
                    throw new EntityEditPersistFailException();
                }
            }
        }

        public async Task<IEnumerable<WatchListMovieViewModel>> GetUserWatchListAsync(string userId)
        {
            return await dbContext.UsersMovies
                .Where(um => um.UserId == userId && um.IsDeleted == false)
                .Select(um => new WatchListMovieViewModel()
                {
                    MovieId = um.Movie.Id,
                    Title = um.Movie.Title,
                    Genre = um.Movie.Genre.ToString(),
                    ReleaseDate = um.Movie.ReleaseDate.ToString("dd/MM/yyyy"),
                    ImageUrl = um.Movie.ImageUrl
                })
                .ToArrayAsync();
        }

        public async Task RemoveMovieFromUserWatchlistAsync(string userId, Guid movieId)
        {
            UserMovie? userMovie = await watchlistRepository.GetUserMovieAsync(userId, movieId);

            if (userMovie == null || userMovie.IsDeleted)
            {
                throw new EntityNotFoundException();
            }

            bool successRemove = await watchlistRepository.SoftDeleteUserMovieAsync(userMovie);

            if (!successRemove)
            {
                throw new InvalidOperationException("Operation failed: Could not remove movie from watchlist.");
            }
        }

        public async Task<bool> MovieIsInUserWatchlistAsync(string userId, Guid movieId)
        {
            return await dbContext.UsersMovies
                .AnyAsync(um => um.UserId == userId && um.MovieId == movieId && um.IsDeleted == false);
        }
    }
}