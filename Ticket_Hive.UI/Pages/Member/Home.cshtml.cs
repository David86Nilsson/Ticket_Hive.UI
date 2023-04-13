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
        private readonly IBookingRepo bookingRepo;


        public HomeModel(IEventModelRepo eventsService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IBookingRepo bookingRepo)
        {
            EventsService = eventsService;
            _userManager = userManager;
            _signInManager = signInManager;
            this.bookingRepo = bookingRepo;
        }

        public List<EventModel>? popularEvents { get; set; }
        public bool IsAdmin { get; set; }
        public List<BookingModel>? ConfirmedBookings { get; set; }


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

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                ConfirmedBookings = await bookingRepo.GetConfirmedBookingsByUserNameAsync(userName);
            }

        }

        public async Task<IActionResult> OnPostSignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }

}