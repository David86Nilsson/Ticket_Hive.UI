using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.UI.Pages.Member
{
    public class EventDetailsModel : PageModel
    {
        public IEventModelRepo EventsService { get; }

        public EventDetailsModel(IEventModelRepo eventsService)
        {
            EventsService = eventsService;
        }

        public EventModel Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Event = await EventsService.GetEventByIdAsync(id);

            if (Event == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
