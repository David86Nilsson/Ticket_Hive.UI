using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;
using Ticket_Hive.Logic;

namespace Ticket_Hive.UI.Pages.Member
{
    public class EvenemangModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IEventModelRepo eventRepo;
        private readonly IAppUserModelRepo appUserModelRepo;
        private readonly IShoppingCartModelRepo cartModelRepo;
        private readonly IBookingRepo bookingRepo;

        public int Id { get; set; }
        [BindProperty]
        public int Tickets { get; set; }
        public int TicketsLeft { get; set; }
        public string? CookieValue { get; set; }
        public EventModel? EventToShow { get; set; }
        public EventManager? EventManager { get; set; }
        public CookieManager? CookieManager { get; set; }
        public ShoppingCartModel? ShoppingCart { get; set; }
        public AppUserModel? AppUser { get; set; }

        public List<CartCookieModel> cartCookieList;



        public EvenemangModel(SignInManager<IdentityUser> signInManager, IEventModelRepo eventRepo, IAppUserModelRepo appUserModelRepo, IBookingRepo bookingRepo)
        {
            this.signInManager = signInManager;
            this.eventRepo = eventRepo;
            this.appUserModelRepo = appUserModelRepo;
            this.bookingRepo = bookingRepo;
            EventManager = new();
            CookieManager = new(appUserModelRepo, eventRepo, bookingRepo, signInManager, HttpContext);
        }
        public async Task OnGet(int id)
        {
            Id = id;
            EventToShow = await eventRepo.GetEventByIdAsync(Id);
            if (EventToShow != null && EventManager != null)
            {
                TicketsLeft = EventManager.TicketsLeft(EventToShow);
            }

            // Get user
            string? userName = await signInManager.UserManager.GetUserNameAsync(await signInManager.UserManager.GetUserAsync(HttpContext.User));
            if (string.IsNullOrEmpty(userName))
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
            if (ModelState.IsValid && AppUser != null && EventToShow != null && ShoppingCart != null && Tickets < TicketsLeft)
            {
                BookingModel newBooking = new()
                {
                    Event = EventToShow!,
                    NbrOfTickets = Tickets,
                    User = AppUser
                };
                ShoppingCart.Bookings.Add(newBooking);
                await CookieManager.SetShoppingCartToCookieAsync(ShoppingCart);
            }
            return RedirectToPage();
        }

        private async Task GetCookieAsync()
        {
            var cookie = HttpContext.Session.GetString("ShoppingCart");
            if (string.IsNullOrEmpty(cookie))
            {
                ShoppingCart = new()
                {
                    User = AppUser
                };
                cartCookieList = new();
            }
            else
            {
                cartCookieList = JsonConvert.DeserializeObject<List<CartCookieModel>>(cookie);
                if (cartCookieList != null)
                {
                    foreach (CartCookieModel cartCookieModel in cartCookieList)
                    {
                        if (cartCookieModel.UserName == AppUser.Username)
                        {
                            ShoppingCart = cartCookieModel.ShoppingCart;
                            break;
                        }
                    }
                }
                else if (ShoppingCart == null)
                {
                    cartCookieList = new();
                    ShoppingCart = new()
                    {
                        User = AppUser
                    };
                }
            }
        }
        private async Task SetCookieAsync(ShoppingCartModel cart)
        {
            CartCookieModel? cartCookie = cartCookieList.FirstOrDefault(cookie => cookie.UserName == cart.User.Username);
            if (cartCookie == null)
            {
                cartCookieList.Add(new CartCookieModel() { UserName = AppUser.Username, ShoppingCart = cart });
            }
            else
            {
                cartCookie.ShoppingCart = cart;
            }
            CookieOptions options = new();
            options.Expires = DateTime.Now.AddMinutes(10);
            var cookieValue = JsonConvert.SerializeObject(cartCookieList);
            HttpContext.Session.SetString("ShoppingCart", cookieValue);

        }
    }
}
