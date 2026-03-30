
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CinemaApp.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime Birthdate { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
            = new List<Ticket>();

        public ICollection<UserMovie> UserWatchlist { get; set; }
            = new List<UserMovie>();
    }
}
