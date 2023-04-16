using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.UI.Pages.Member
{
    /// <summary>
    ///  HomeModel-klassen som ärver från PageModel.
    /// Denna klass kräver att användaren är auktoriserad.
    /// </summary>
    [Authorize]
    public class HomeModel : PageModel
    {
        /// <summary>
        /// Hämtar en instans av IEventModelRepo som används för att hantera event.
        /// </summary>
        public IEventModelRepo EventsService { get; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IBookingRepo bookingRepo;

        [BindProperty]
        public string SearchWord { get; set; }

        /// <summary>
        /// Skapar en ny instans av HomeModel-klassen.
        /// </summary>
        /// <param name="eventsService">En instans av IEventModelRepo för hantering av händelser.</param>
        /// <param name="userManager">En instans av UserManager för hantering av användare.</param>
        /// <param name="signInManager">En instans av SignInManager för hantering av inloggning.</param>
        /// <param name="bookingRepo">En instans av IBookingRepo för att hantera bokningar.</param>
        public HomeModel(IEventModelRepo eventsService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IBookingRepo bookingRepo)
        {
            EventsService = eventsService;
            _userManager = userManager;
            _signInManager = signInManager;
            this.bookingRepo = bookingRepo;
        }
        public List<EventModel> RecommendedEvents { get; set; }

        /// <summary>
        /// Hämtar eller sätter en lista med populära events.
        /// </summary>
        public List<EventModel>? popularEvents { get; set; }

        /// <summary>
        /// Hämtar eller sätter om användaren är admin.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Hämtar eller sätter en lista med bekräftade bokningar.
        /// </summary>
        public List<BookingModel>? ConfirmedBookings { get; set; }

        /// <summary>
        /// Hanterar GET-begäran för hemsidan.
        /// </summary>
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

        /// <summary>
        /// Hanterar POST-begäran för att logga ut användaren.
        /// </summary>
        public async Task<IActionResult> OnPostSignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }

}