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
        public List<EventModel> Events { get; set; } = new()
        {
           new EventModel()
           {

               Location = "Malm�",
               Price = 500,
               Capacity = 10000,

           },

           new EventModel()
           {
               Location = "G�teborg",
               Price = 1100,
               Capacity = 30000,

           }
        };

        public ConfirmationPageModel(SignInManager<IdentityUser> signInManager, IEventModelRepo eventRepo, IAppUserModelRepo appUserModelRepo, IBookingRepo bookingRepo)
        {
            // H�mta repos och managers
            this.signInManager = signInManager;
            this.eventRepo = eventRepo;
            this.appUserModelRepo = appUserModelRepo;
            this.bookingRepo = bookingRepo;
            eventManager = new();
            cookieManager = new(appUserModelRepo, eventRepo, bookingRepo, signInManager, HttpContext);
        }
        public async Task OnGet()
        {
            // H�mta Shoppingcart fr�n cookie

            Shoppingcart = await cookieManager.GetShoppingCartFromCookieAsync();

            // Ta bort shoppingcart fr�n cookie

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
