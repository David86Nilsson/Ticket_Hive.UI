using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public class EventModelRepo : IEventModelRepo
    {
        public Task<bool> AddEventAsync(EventModel newEvent)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEventAsync(EventModel eventToDelete)
        {
            throw new NotImplementedException();
        }

        public Task<List<EventModel>?> GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EventModel?> GetEventByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEventAsync(EventModel updatedEvent)
        {
            throw new NotImplementedException();
        }
    }
}
