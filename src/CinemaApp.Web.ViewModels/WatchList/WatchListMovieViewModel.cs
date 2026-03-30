
namespace CinemaApp.Web.ViewModels.WatchList
{
    public class WatchListMovieViewModel
    {
        public Guid MovieId { get; set; }

        public string Title { get; set; }= null!;

        public string Genre { get; set; } = null!;

        public string ReleaseDate { get; set; } = null!;


        public string? ImageUrl { get; set; }
    }
}
