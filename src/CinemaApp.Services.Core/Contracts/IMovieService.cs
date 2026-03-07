

using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Services.Core.Contracts
{
    public interface IMovieService
    {
        Task<IEnumerable<AllMoviesIndexViewModel>> GetAllMoviesOrderedByTitleAsync();

        Task CreateMovieAsync(MovieFormModel formModel);

        Task<MovieDetailsViewModel?> GetMovieDetailsByIdAsync(Guid id);

        Task<MovieFormModel> GetMovieFormModelByIdAsync(Guid id);
        Task EditMovieAsync(Guid id, MovieFormModel formModel);

        Task<bool> ExistsByIdAsync(Guid id);

    }
}
