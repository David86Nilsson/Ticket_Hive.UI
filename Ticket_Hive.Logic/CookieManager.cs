using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.Logic
{
    public class CookieManager
    {
        private readonly IAppUserModelRepo appUserModelRepo;
        private readonly IEventModelRepo eventModelRepo;
        private readonly IBookingRepo bookingModelRepo;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly HttpContext httpContext;

        public CookieManager(IAppUserModelRepo appUserModelRepo, IEventModelRepo eventModelRepo, IBookingRepo bookingModelRepo, SignInManager<IdentityUser> signInManager, HttpContext httpContext)
        {
            this.appUserModelRepo = appUserModelRepo;
            this.eventModelRepo = eventModelRepo;
            this.bookingModelRepo = bookingModelRepo;
            this.signInManager = signInManager;
            this.httpContext = httpContext;
        }

        public async Task SetShoppingCartToCookieAsync(ShoppingCartModel cart)
        {
            var cookie = httpContext.Session.GetString("ShoppingCart");
            var cartCookieList = JsonConvert.DeserializeObject<List<CartCookieModel>>(cookie);
            CartCookieModel? cartCookie = cartCookieList.FirstOrDefault(cookie => cookie.UserName == cart.User.Username);
            if (cartCookie == null)
            {
                AppUserModel? appUser = await appUserModelRepo.GetUserByUserNameAsync(await signInManager.UserManager.GetUserNameAsync(await signInManager.UserManager.GetUserAsync(httpContext.User)));
                cartCookieList.Add(new CartCookieModel() { UserName = appUser.Username, ShoppingCart = cart });
            }
            else
            {
                cartCookie.ShoppingCart = cart;
            }

            var cookieValue = JsonConvert.SerializeObject(cartCookieList);
            httpContext.Session.SetString("ShoppingCart", cookieValue);
        }
        public async Task<ShoppingCartModel?> GetShoppingCartFromCookieAsync()
        {
            AppUserModel? AppUser = null;
            string? userName = await signInManager.UserManager.GetUserNameAsync(await signInManager.UserManager.GetUserAsync(httpContext.User));
            if (string.IsNullOrEmpty(userName))
            {
                AppUser = await appUserModelRepo.GetUserByUserNameAsync(userName);
                if (AppUser == null)
                {
                    return null;
                }

                var cookie = httpContext.Session.GetString("ShoppingCart");
                if (string.IsNullOrEmpty(cookie))
                {
                    return new();
                }
                var cartCookieList = JsonConvert.DeserializeObject<List<CartCookieModel>>(cookie);
                if (cartCookieList == null)
                {
                    return new();
                }
                var cartCookie = cartCookieList.FirstOrDefault(cc => cc.UserName == AppUser.Username);
                if (cartCookie == null)
                {
                    return new();
                }
                return cartCookie.ShoppingCart;
            }
            return null;
        }
    }
}
