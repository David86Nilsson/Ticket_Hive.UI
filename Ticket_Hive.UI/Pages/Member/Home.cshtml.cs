using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.UI.Pages.Member
{
    [Authorize]
    public class HomeModel : PageModel
    {
        public IEventModelRepo EventsService { get; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public HomeModel(IEventModelRepo eventsService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            EventsService = eventsService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public List<EventModel>? popularEvents { get; set; }
        public List<EventModel>? eventsBought { get; set; }
        public string SearchWord { get; set; }
        public List<EventModel>? RecommendedEvents { get; set; }
        public bool IsAdmin { get; set; }



        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            }

            List<EventModel> allEvents = await EventsService.GetAllEventsAsync();
            List<EventModel> sortedPopularEvents = allEvents.OrderByDescending(s => s.TicketsSold).ToList();
            popularEvents = sortedPopularEvents.Take(3).ToList();
         
            var name = User.Identity.Name;
            eventsBought = await EventsService.GetAllEventsFromUserAsync(name);

            RecommendedEvents = eventsBought.Take(3).ToList();

        }

        public IActionResult OnPostShowEvent(int id)
        {
            return RedirectToPage("/EventDetails", new { id });
        }

        public IActionResult OnPostShowAllEvents()
        {
            return RedirectToPage("/Member/BookingPage");
        }

        public IActionResult OnPostSearchForEvent()
        {
            return RedirectToPage("/Booking", new { searchWord = SearchWord });
        }

        public async Task<IActionResult> OnPostSignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }

}