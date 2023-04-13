using Microsoft.EntityFrameworkCore;
using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public class EventModelRepo : IEventModelRepo
    {
        private readonly EventDbContext context;

        public EventModelRepo(EventDbContext context)
        {
            this.context = context;
        }
        public async Task AddEventAsync(EventModel newEvent)
        {
            await context.Events.AddAsync(newEvent);
            await context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(EventModel eventToDelete)
        {
            EventModel? existingEvent = await context.Events.FirstOrDefaultAsync(e => e.Id == eventToDelete.Id);
            if (existingEvent != null)
            {
                context.Events.Remove(existingEvent);
                await context.SaveChangesAsync();
            }

        }

        public async Task<List<EventModel>?> GetAllEventsAsync()
        {
            return await context.Events.Include(e => e.Users).OrderBy(e => e.DateTime).ToListAsync();

        }

        public async Task<EventModel?> GetEventByIdAsync(int id)
        {
            return await context.Events.Include(e => e.Users).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<bool> UpdateEventAsync(EventModel updatedEvent)
        {
            EventModel? existingEvent = await context.Events.FirstOrDefaultAsync(e => e.Id == updatedEvent.Id);
            if (existingEvent != null)
            {
                existingEvent.EventType = updatedEvent.EventType;
                existingEvent.Price = updatedEvent.Price;
                existingEvent.Location = updatedEvent.Location;
                existingEvent.Capacity = updatedEvent.Capacity;
                existingEvent.TicketsSold = updatedEvent.TicketsSold;
                existingEvent.Users = updatedEvent.Users;
                existingEvent.Name = updatedEvent.Name;
                existingEvent.DateTime = updatedEvent.DateTime;

                context.Events.Update(existingEvent);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<EventModel>?> GetAllEventsFromUserAsync(string username)
        {
            AppUserModel user = await context.AppUsers
                .Include(u => u.Events)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
            {
                return user.Events;
            }

            return null;
        }

        public async Task<bool> AddEventToUserAsync(string username, int eventId)
        {
            AppUserModel user = await context.AppUsers.FirstOrDefaultAsync(u => u.Username == username);
            EventModel eventModel = await context.Events.FirstOrDefaultAsync(e => e.Id == eventId);

            if (user != null && eventModel != null)
            {
                user.Events.Add(eventModel);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }

}
