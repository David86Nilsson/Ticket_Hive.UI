using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;
using Ticket_Hive.Logic;

namespace Ticket_Hive.UI.Pages.Member
{
    public class ConfirmationPageModel : PageModel
    {
        public CookieManager cookieManager;
        public EventManager eventManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IEventModelRepo eventRepo;
        private readonly IAppUserModelRepo appUserModelRepo;
        private readonly IBookingRepo bookingRepo;

        public ShoppingCartModel? Shoppingcart { get; set; }

        public ConfirmationPageModel(SignInManager<IdentityUser> signInManager, IEventModelRepo eventRepo, IAppUserModelRepo appUserModelRepo, IBookingRepo bookingRepo)
        {
            // Hämta repos och managers
            this.signInManager = signInManager;
            this.eventRepo = eventRepo;
            this.appUserModelRepo = appUserModelRepo;
            this.bookingRepo = bookingRepo;
            eventManager = new();
            cookieManager = new();
        }
        public async Task OnGet()
        {
            // Hämta Shoppingcart från cookie
            cookieManager.SetAttributesToCookieManager(appUserModelRepo, eventRepo, bookingRepo, signInManager, HttpContext);

            Shoppingcart = await cookieManager.GetShoppingCartFromCookieAsync();

            // Ta bort shoppingcart från cookie

            ShoppingCartModel emptyShoppingCart = new()
            {
                User = Shoppingcart.User,
                Bookings = new(),
                Created = DateTime.Now
            };
            await cookieManager.SetShoppingCartToCookieAsync(emptyShoppingCart);
        }
    }
}
