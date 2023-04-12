using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;
using Ticket_Hive.Logic;

namespace Ticket_Hive.UI.Pages.Member
{
    public class ShoppingCartPageModel : PageModel
    {

        [BindProperty]
        public ShoppingCartModel? ShoppingCart { get; set; }
        public EventManager? eventManager;
        public CookieManager? cookieManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IBookingRepo bookingRepo;
        private readonly IEventModelRepo eventModelRepo;
        private readonly IAppUserModelRepo appUserModelRepo;

        public AppUserModel? AppUser { get; set; }
        [BindProperty]
        public int NewQuantity { get; set; }
        [BindProperty]
        public int EventId { get; set; }

        public ShoppingCartPageModel(SignInManager<IdentityUser> signInManager, IBookingRepo bookingRepo, IEventModelRepo eventModelRepo, IAppUserModelRepo appUserModelRepo)
        {
            eventManager = new();
            cookieManager = new();
            this.signInManager = signInManager;
            this.bookingRepo = bookingRepo;
            this.eventModelRepo = eventModelRepo;
            this.appUserModelRepo = appUserModelRepo;
        }
        public async Task OnGet()
        {
            cookieManager.SetAttributesToCookieManager(appUserModelRepo, eventModelRepo, bookingRepo, signInManager, HttpContext);
            ShoppingCart = await cookieManager.GetShoppingCartFromCookieAsync();
        }
        public async Task<IActionResult> OnPostBuy()
        {
            cookieManager.SetAttributesToCookieManager(appUserModelRepo, eventModelRepo, bookingRepo, signInManager, HttpContext);
            ShoppingCart = await cookieManager.GetShoppingCartFromCookieAsync();
            // Remove bookings with 0 tickets
            var bookings = ShoppingCart.Bookings;
            for (int i = 0; i < ShoppingCart.Bookings.Count; i++)
            {
                BookingModel booking = ShoppingCart.Bookings[i];
                if (booking.NbrOfTickets == 0)
                {
                    ShoppingCart.Bookings.Remove(booking);
                }
            }
            //await cookieManager.SetShoppingCartToCookieAsync(ShoppingCart);
            // Buy tickets and save to database
            if (ShoppingCart.Bookings.Count > 0)
            {
                await eventManager.BuyTicketsAsync(ShoppingCart, bookingRepo, eventModelRepo, appUserModelRepo, ShoppingCart.User);
                return RedirectToPage("/Member/ConfirmationPage");
            }
            //comment
            return Page();
        }

        public async Task<IActionResult> OnPostCookie()
        {
            cookieManager.SetAttributesToCookieManager(appUserModelRepo, eventModelRepo, bookingRepo, signInManager, HttpContext);
            ShoppingCart = await cookieManager.GetShoppingCartFromCookieAsync();
            BookingModel booking = ShoppingCart.Bookings.FirstOrDefault(b => b.Event.Id == EventId);
            if (booking != null)
            {
                if (NewQuantity == 0)
                {
                    ShoppingCart.Bookings.Remove(booking);
                }
                else
                {
                    booking.NbrOfTickets = NewQuantity;
                }
                await cookieManager.SetShoppingCartToCookieAsync(ShoppingCart);
            }

            return Page();
        }
    }
}




