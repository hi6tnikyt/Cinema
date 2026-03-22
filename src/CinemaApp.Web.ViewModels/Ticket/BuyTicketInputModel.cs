
namespace CinemaApp.Web.ViewModels.Ticket
{
    public class BuyTicketInputModel
    {
        public Guid CinemaId { get; set; }

        public Guid MovieId { get; set; }    

        public int Quantity { get; set; }

        public DateTime Showtime { get; set; }
    }
}
