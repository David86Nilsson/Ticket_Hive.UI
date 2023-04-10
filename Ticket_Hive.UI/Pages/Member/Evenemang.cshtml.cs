using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;
using Ticket_Hive.Logic;

namespace Ticket_Hive.UI.Pages.Member
{
    [BindProperties]
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



        public EvenemangModel(SignInManager<IdentityUser> signInManager, IEventModelRepo eventRepo, IAppUserModelRepo appUserModelRepo, IBookingRepo bookingRepo)
        {
            this.signInManager = signInManager;
            this.eventRepo = eventRepo;
            this.appUserModelRepo = appUserModelRepo;
            this.bookingRepo = bookingRepo;
            EventManager = new();
            CookieManager = new();
        }
        public async Task OnGet(int id)
        {
            CookieManager.SetAttributesToCookieManager(appUserModelRepo, eventRepo, bookingRepo, signInManager, HttpContext);
            //Id = 2; // new Random().Next(1, 6);

            //Andre
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
        public async Task<IActionResult> OnPost()
        {

            if (AppUser != null && EventToShow != null && ShoppingCart != null && Tickets < TicketsLeft)
            {
                CookieManager.SetAttributesToCookieManager(appUserModelRepo, eventRepo, bookingRepo, signInManager, HttpContext);
                BookingModel existingBooking = ShoppingCart.Bookings.FirstOrDefault(b => b.EventId == Id);
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
                        User = AppUser
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
