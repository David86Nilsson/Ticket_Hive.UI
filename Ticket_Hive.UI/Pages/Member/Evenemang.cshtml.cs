
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;
using Ticket_Hive.Logic;

namespace Ticket_Hive.UI.Pages.Member
{
    [BindProperties]
    /// <summary>
    /// Represents the page model for the event details page.
    /// </summary>
    public class EvenemangModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IEventModelRepo eventRepo;
        private readonly IAppUserModelRepo appUserModelRepo;
        private readonly IBookingRepo bookingRepo;

        public int Id { get; set; }

        public int TicketsLeft { get; set; }
        public string? CookieValue { get; set; }
        public EventModel? EventToShow { get; set; }
        public EventManager? EventManager { get; set; }
        public CookieManager? CookieManager { get; set; }
        public ShoppingCartModel? ShoppingCart { get; set; }
        public AppUserModel? AppUser { get; set; }

        public List<CartCookieModel> cartCookieList;

        //[BindProperty]
        public int Tickets { get; set; }


        /**
        <summary>Constructor for the EvenemangModel class.</summary>
        <param name="signInManager">The SignInManager object for user authentication and management.</param>
        <param name="eventRepo">The IEventModelRepo object for accessing event data.</param>
        <param name="appUserModelRepo">The IAppUserModelRepo object for accessing application user data.</param>
        <param name="bookingRepo">The IBookingRepo object for accessing booking data.</param>
        */
        public EvenemangModel(SignInManager<IdentityUser> signInManager, IEventModelRepo eventRepo, IAppUserModelRepo appUserModelRepo, IBookingRepo bookingRepo)
        {
            this.signInManager = signInManager;
            this.eventRepo = eventRepo;
            this.appUserModelRepo = appUserModelRepo;
            this.bookingRepo = bookingRepo;
            EventManager = new();
            CookieManager = new();
        }

        /// <summary>
        /// Handles the HTTP GET request for the page.
        /// Retrieves the event with the specified ID and sets the TicketsLeft property based on the number of available tickets for the event.
        /// Gets the currently logged in user and their associated shopping cart from the cookie.
        /// </summary>
        /// <param name="id">The ID of the event to retrieve.</param>
        public async Task OnGet(int id)
        {
            CookieManager.SetAttributesToCookieManager(appUserModelRepo, eventRepo, bookingRepo, signInManager, HttpContext);
            Id = id;

            EventToShow = await eventRepo.GetEventByIdAsync(Id);
            if (EventToShow != null && EventManager != null)
            {
                TicketsLeft = EventManager.TicketsLeft(EventToShow);
            }

            // Get user
            var user = await signInManager.UserManager.GetUserAsync(HttpContext.User);
            string? userName = user.UserName;
            if (!string.IsNullOrEmpty(userName))
            {
                AppUser = await appUserModelRepo.GetUserByUserNameAsync(userName);
            }

            //Get CookieInfo
            if (AppUser != null)
            {
                ShoppingCart = await CookieManager.GetShoppingCartFromCookieAsync();
            }

        }

        ///<summary>
        ///Handles the HTTP POST request to add an event booking to the shopping cart
        ///.</summary>
        ///<returns>
        ///Returns an IActionResult representing the result of the operation.
        ///</returns>
        public async Task<IActionResult> OnPost()
        {
            CookieManager.SetAttributesToCookieManager(appUserModelRepo, eventRepo, bookingRepo, signInManager, HttpContext);

            EventToShow = await eventRepo.GetEventByIdAsync(Id);
            if (EventToShow != null && EventManager != null)
            {
                TicketsLeft = EventManager.TicketsLeft(EventToShow);
            }

            // Get user
            var user = await signInManager.UserManager.GetUserAsync(HttpContext.User);
            string? userName = user.UserName;
            if (!string.IsNullOrEmpty(userName))
            {
                AppUser = await appUserModelRepo.GetUserByUserNameAsync(userName);
            }

            //Get CookieInfo
            if (AppUser != null)
            {
                ShoppingCart = await CookieManager.GetShoppingCartFromCookieAsync();
            }

            if (AppUser != null && EventToShow != null && ShoppingCart != null && Tickets <= TicketsLeft)
            {

                // Check if evnt is already booked
                BookingModel existingBooking = ShoppingCart.Bookings.FirstOrDefault(b => b.Event.Id == Id);
                if (existingBooking != null)
                {
                    existingBooking.NbrOfTickets += Tickets;
                }
                else
                {
                    BookingModel newBooking = new()
                    {
                        Event = EventToShow,
                        NbrOfTickets = Tickets,
                    };
                    ShoppingCart.Bookings.Add(newBooking);
                }
                await CookieManager.SetShoppingCartToCookieAsync(ShoppingCart);
                return RedirectToPage("/Member/Home");
            }
            return Page();
        }
    }
}
