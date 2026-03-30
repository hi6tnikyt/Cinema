namespace CinemaApp.Web.ViewModels.Cinema
{
    public class CinemaProgramViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public ICollection<CinemaProgramMoviesViewModel> ProjectionsMovies { get; set; } = new HashSet<CinemaProgramMoviesViewModel>();
    }
}