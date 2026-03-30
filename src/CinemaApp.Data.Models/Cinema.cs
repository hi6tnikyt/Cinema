
using static CinemaApp.Data.Common.EntityValidation.Cinema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaApp.Data.Models
{
    public class Cinema
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(LocationMaxLength)]
        public string Location { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(Manager))]
        public Guid? ManagerId { get; set; }

        public virtual Manager Manager { get; set; } = null!;

        public virtual ICollection<Projection> Projections { get; set; }
            = new HashSet<Projection>();
    }
}
