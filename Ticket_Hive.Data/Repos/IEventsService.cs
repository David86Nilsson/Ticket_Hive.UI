using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public interface IEventsService
    {
        Task<List<EventModel>?> GetAllEventsAsync();
        Task<EventModel?> GetEventByIdAsync(int id);
        Task<List<EventModel>?> GetAllEventsFromUserAsync(string username);
        Task<EventModel?> UpdateEventAsync(EventModel eventToUpdate);
        Task<bool> AddEventAsync(EventModel eventToAdd);
        Task<bool> AddEventToUserAsync(string username, int eventId);
        Task<bool> DeleteEventAsync(int id);
    }
}
