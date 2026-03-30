

using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Services.Core.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync(string? userId = null, string? searchQuery = null, int pageNumber = 1, int pageSize = 5);

        Task CreateMovieAsync(MovieFormModel formModel);

        Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id);

        Task<MovieFormModel> GetMovieFormModelByIdAsync(Guid id);
        Task EditMovieAsync(Guid id, MovieFormModel formModel);

        Task<bool> ExistsByIdAsync(Guid id);

        Task HardDeleteMovieAsync(Guid id);
        Task SoftDeleteMovieAsync(Guid id);

        Task<int> GetMoviesCountAsync(string? searchQuery = null);
    }
}
