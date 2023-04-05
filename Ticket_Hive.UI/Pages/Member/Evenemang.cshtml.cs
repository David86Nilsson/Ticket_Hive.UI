using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public int Id { get; set; }
        [BindProperty]
        public int Tickets { get; set; }
        public int TicketsLeft { get; set; }
        public string? CookieValue { get; set; }
        public EventModel? EventToShow { get; set; }
        public EventManager? EventManager { get; set; }
        public ShoppingCartModel? ShoppingCart { get; set; }
        public AppUserModel? AppUser { get; set; }


        public EvenemangModel(SignInManager<IdentityUser> signInManager, IEventModelRepo eventRepo, IAppUserModelRepo appUserModelRepo, IShoppingCartModelRepo cartModelRepo)
        {
            this.signInManager = signInManager;
            this.eventRepo = eventRepo;
            this.appUserModelRepo = appUserModelRepo;
            this.cartModelRepo = cartModelRepo;
            EventManager = new();
        }
        public async Task OnGet(int id)
        {
            Id = id;
            EventToShow = await eventRepo.GetEventByIdAsync(Id);
            if (EventToShow != null)
            {
                TicketsLeft = EventManager.TicketsLeft(EventToShow);
            }

            string? userName = await signInManager.UserManager.GetUserNameAsync(await signInManager.UserManager.GetUserAsync(HttpContext.User));
            if (string.IsNullOrEmpty(userName))
            {
                AppUser = await appUserModelRepo.GetUserByUserNameAsync(userName);
            }

            //Get CookieInfo
            CookieValue = Request.Cookies["ShoppingListCookie"];
            if (string.IsNullOrEmpty(CookieValue))
            {
                await CreateNewCookie();
            }
            else
            {
                bool IsValue = int.TryParse(CookieValue, out int cartId);
                if (IsValue)
                {
                    ShoppingCart = await cartModelRepo.GetShoppingCartByIdAsync(cartId);
                    if (ShoppingCart == null)
                    {
                        await CreateNewCookie();
                    }
                }
                else
                {
                    await CreateNewCookie();
                }
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
                await cartModelRepo.UpdateShoppingCartAsync(ShoppingCart);
            }
            return RedirectToPage();
        }
        private async Task CreateNewCookie()
        {
            if (AppUser != null)
            {
                CookieOptions options = new();
                options.Expires = DateTime.Now.AddMinutes(10);
                await cartModelRepo.AddShoppingCartAsync(new ShoppingCartModel() { User = AppUser, Created = DateTime.Now });
                ShoppingCart = await cartModelRepo.GetMostRecentShoppingCartAsync();
                Response.Cookies.Append("ShoppingListCookie", ShoppingCart.Id.ToString(), options);
            }
        }
    }
}
