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

        public ShoppingCartPageModel(SignInManager<IdentityUser> signInManager, IBookingRepo bookingRepo, IEventModelRepo eventModelRepo, IAppUserModelRepo appUserModelRepo)
        {
            eventManager = new();
            this.signInManager = signInManager;
            this.bookingRepo = bookingRepo;
            this.eventModelRepo = eventModelRepo;
            this.appUserModelRepo = appUserModelRepo;
            cookieManager = new(appUserModelRepo, eventModelRepo, bookingRepo, signInManager, HttpContext);


        }
        public async Task OnGet()
        {
            // Get shoppingcart from cookie
            //string? userName = await signInManager.UserManager.GetUserNameAsync(await signInManager.UserManager.GetUserAsync(HttpContext.User));
            //if (string.IsNullOrEmpty(userName))
            //{
            //    AppUser = await appUserModelRepo.GetUserByUserNameAsync(userName);
            //}
            //var cookie = HttpContext.Session.GetString("ShoppingCart");
            //var cartCookieList = JsonConvert.DeserializeObject<List<CartCookieModel>>(cookie);
            //var cartCookie = cartCookieList.FirstOrDefault(cc => cc.UserName == AppUser.Username);
            //ShoppingCart = cartCookie.ShoppingCart;
            ShoppingCart = await cookieManager.GetShoppingCartFromCookie();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                // Buy tickets
                await eventManager.BuyTicketsAsync(ShoppingCart, bookingRepo, eventModelRepo, appUserModelRepo);
                // Go To Confirmation Page
                return RedirectToPage("/Member/ConfirmationPage");
            }
            return Page();
        }

    }
}
