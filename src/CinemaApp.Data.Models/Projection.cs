

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CinemaApp.Data.Common.EntityValidation.Projection;

namespace CinemaApp.Data.Models
{
    public class Projection
    {
        [Key]
        public Guid Id { get; set; } 

        [ForeignKey(nameof(Cinema))]
        public Guid CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; } = null!;

        [ForeignKey(nameof(Movie))]
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public TimeOnly Showtime { get; set; }

        public int AvailableTickets { get; set; }

        [Column(TypeName = TicketPriceType)]
        public decimal TicketPrice { get; set; }    

        public virtual ICollection<Ticket> Tickets { get; set; }
            = new List<Ticket>();
    }
}
