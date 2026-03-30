
namespace CinemaApp.Web.ViewModels.Movie
{
    public class MovieAllIndexViewModel
    {
        public string? SearchQuery { get; set; }

        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public ICollection<AllMoviesIndexViewModel> Movies { get; set; } 
            = new List<AllMoviesIndexViewModel>();
    }
}
