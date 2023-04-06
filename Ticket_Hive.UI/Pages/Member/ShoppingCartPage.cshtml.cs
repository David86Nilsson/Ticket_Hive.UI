using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
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
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IBookingRepo bookingRepo;
        private readonly IEventModelRepo eventModelRepo;
        private readonly IAppUserModelRepo appUserModelRepo;

        public AppUserModel? AppUser { get; set; }

        public ShoppingCartPageModel(SignInManager<IdentityUser> signInManager, IBookingRepo bookingRepo, IEventModelRepo eventModelRepo, IAppUserModelRepo appUserModelRepo
        {
            eventManager = new();
            this.signInManager = signInManager;
            this.bookingRepo = bookingRepo;
            this.eventModelRepo = eventModelRepo;
            this.appUserModelRepo = appUserModelRepo;
        }
        public async Task OnGet()
        {
            // Get shoppingcart from cookie
            string? userName = await signInManager.UserManager.GetUserNameAsync(await signInManager.UserManager.GetUserAsync(HttpContext.User));
            if (string.IsNullOrEmpty(userName))
            {
                AppUser = await appUserModelRepo.GetUserByUserNameAsync(userName);
            }
            var cookie = HttpContext.Session.GetString("ShoppingCart");
            var cartCookieList = JsonConvert.DeserializeObject<List<CartCookieModel>>(cookie);
            var cartCookie = cartCookieList.FirstOrDefault(cc => cc.UserName == AppUser.Username);
            ShoppingCart = cartCookie.ShoppingCart;
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
