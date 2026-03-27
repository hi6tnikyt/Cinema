
namespace CinemaApp.Web.ViewModels.ApplicationUser
{
    public class UserManagerAll
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; }
           = new List<string>();
    }
}
