using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;


namespace Ticket_Hive.UI.Pages.Member
{
    public class HomeModel : PageModel
    {
        public IEventsService EventsService { get; }
        public AuthenticationStateProvider Provider { get; }

        public HomeModel(IEventsService eventsService, AuthenticationStateProvider provider)
        {
            EventsService = eventsService;
            Provider = provider;
        }

        public List<EventModel>? popularEvents { get; set; }
        public List<EventModel>? eventsBought { get; set; }
        public string SearchWord { get; set; }


        public async Task OnGetAsync()
        {
            // Add your initialization code here, similar to what you have in OnInitializedAsync
            // Hämtar alla populära events, sorterar dom och visar top 3 baserat på sålda biljetter
            List<EventModel> allEvents = await EventsService.GetAllEventsAsync();
            List<EventModel> sortedPopularEvents = allEvents.OrderByDescending(s => s.TicketsSold).ToList();
            popularEvents = sortedPopularEvents.Take(3).ToList();

            // Hämtar alla events som användaren har biljetter till
            var authState = await Provider.GetAuthenticationStateAsync();
            var name = authState.User.Identity.Name;
            eventsBought = await EventsService.GetAllEventsFromUserAsync(name);

            RecommendedEvents = eventsBought.Take(3).ToList();

        }

        public IActionResult OnPostShowEvent(int id)
        {
            return RedirectToPage("/EventDetails", new { id });
        }

        public IActionResult OnPostShowAllEvents()
        {
            return RedirectToPage("/Events");
        }

        public IActionResult OnPostSearchForEvent()
        {
            return RedirectToPage("/Events", new { searchWord = SearchWord });
        }
    }

}