
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Data.Models
{
    public class Manager
    {
        [Key]
        public Guid Id { get; set; }
        public int Level { get; set; }
           
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<Cinema> ManagerCinemas { get; set; }
            = new HashSet<Cinema>();
    }
}
