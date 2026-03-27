
using System.Globalization;

namespace CinemaApp.Web.ViewModels.Admin.User
{
    public class AdminManagerUserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;

        public ICollection<string> Roles { get; set; }
           = new List<string>();
    }
}
