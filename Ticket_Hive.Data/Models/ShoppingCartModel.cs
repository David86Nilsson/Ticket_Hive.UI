namespace Ticket_Hive.Data.Models
{
    public class ShoppingCartModel
    {
        public int Id { get; set; }
        public string User { get; set; } = null!;
        public List<BookingModel> Bookings { get; set; } = new();
        public DateTime Created { get; set; }
    }
}
