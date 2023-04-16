using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.UI.Pages.Member
{
    /// <summary>
    ///  HomeModel-klassen som �rver fr�n PageModel.
    /// Denna klass kr�ver att anv�ndaren �r auktoriserad.
    /// </summary>
    [Authorize]
    public class HomeModel : PageModel
    {
        /// <summary>
        /// H�mtar en instans av IEventModelRepo som anv�nds f�r att hantera event.
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
        /// <param name="eventsService">En instans av IEventModelRepo f�r hantering av h�ndelser.</param>
        /// <param name="userManager">En instans av UserManager f�r hantering av anv�ndare.</param>
        /// <param name="signInManager">En instans av SignInManager f�r hantering av inloggning.</param>
        /// <param name="bookingRepo">En instans av IBookingRepo f�r att hantera bokningar.</param>
        public HomeModel(IEventModelRepo eventsService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IBookingRepo bookingRepo)
        {
            EventsService = eventsService;
            _userManager = userManager;
            _signInManager = signInManager;
            this.bookingRepo = bookingRepo;
        }
        public List<EventModel> RecommendedEvents { get; set; }

        /// <summary>
        /// H�mtar eller s�tter en lista med popul�ra events.
        /// </summary>
        public List<EventModel>? popularEvents { get; set; }

        /// <summary>
        /// H�mtar eller s�tter om anv�ndaren �r admin.
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// H�mtar eller s�tter en lista med bekr�ftade bokningar.
        /// </summary>
        public List<BookingModel>? ConfirmedBookings { get; set; }

        /// <summary>
        /// Hanterar GET-beg�ran f�r hemsidan.
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
        /// Hanterar POST-beg�ran f�r att logga ut anv�ndaren.
        /// </summary>
        public async Task<IActionResult> OnPostSignOutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }

}