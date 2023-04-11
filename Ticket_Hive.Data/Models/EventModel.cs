﻿namespace Ticket_Hive.Data.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string EventType { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public string Location { get; set; } = null!;
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public int TicketsSold { get; set; }
        public string? Image { get; set; }
        public List<AppUserModel> Users { get; set; } = new();
        public List<EventModel>? RecommendedEvents { get; set; }
        //public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();

    }
}
