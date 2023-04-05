using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Logic
{
    public class EventManager
    {
        public bool IsEventFullyBooked(EventModel eventModel)
        {
            return eventModel.Capacity <= eventModel.TicketsSold;
        }
        public int TicketsLeft(EventModel eventModel)
        {
            return eventModel.Capacity - eventModel.TicketsSold;
        }
    }
}
