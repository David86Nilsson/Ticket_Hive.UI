namespace Ticket_Hive.Data.Models
{
    public class BookingModel
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int EventId { get; set; }
        public EventModel Event { get; set; } = null!;
        public List<AppUserModel> Users { get; set; } = new();
    }
}
