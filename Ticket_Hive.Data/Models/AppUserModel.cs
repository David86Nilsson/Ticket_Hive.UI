namespace Ticket_Hive.Data.Models
{
    public class AppUserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public List<BookingModel> Bookings { get; set; } = new();
        public List<EventModel> Events { get; set; } = new();
        //public ICollection<UserEvent> UserEvents { get; set; }


    }
}
