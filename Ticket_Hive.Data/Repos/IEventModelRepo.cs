using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public interface IEventModelRepo
    {
        public Task<List<EventModel>?> GetAllEventsAsync();
        public Task<EventModel?> GetEventByIdAsync(int id);
        public Task AddEventAsync(EventModel newEvent);
        public Task<bool> UpdateEventAsync(EventModel updatedEvent);
        public Task DeleteEventAsync(EventModel eventToDelete);

    }
}
