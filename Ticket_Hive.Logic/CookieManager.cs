using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Ticket_Hive.Data.Models;
using Ticket_Hive.Data.Repos;

namespace Ticket_Hive.Logic
{
    public class CookieManager
    {
        private IAppUserModelRepo? appUserModelRepo;
        private IEventModelRepo? eventModelRepo;
        private IBookingRepo? bookingModelRepo;
        private SignInManager<IdentityUser>? signInManager;
        private HttpContext? httpContext;

        public CookieManager()
        {
        }
        public void SetAttributesToCookieManager(IAppUserModelRepo appUserModelRepo, IEventModelRepo eventModelRepo, IBookingRepo bookingModelRepo, SignInManager<IdentityUser> signInManager, HttpContext httpContext)
        {
            this.appUserModelRepo = appUserModelRepo;
            this.eventModelRepo = eventModelRepo;
            this.bookingModelRepo = bookingModelRepo;
            this.signInManager = signInManager;
            this.httpContext = httpContext;
        }

        public async Task SetShoppingCartToCookieAsync(ShoppingCartModel cart)
        {
            var userName = await signInManager.UserManager.GetUserNameAsync(await signInManager.UserManager.GetUserAsync(httpContext.User));

            var cookie = httpContext.Session.GetString("ShoppingCart");
            List<CartCookieModel> cartCookieList;
            if (cookie == null) cartCookieList = new();
            else cartCookieList = JsonConvert.DeserializeObject<List<CartCookieModel>>(cookie);

            CartCookieModel? cartCookie = cartCookieList.FirstOrDefault(c => c.UserName == userName);
            if (cartCookie == null)
            {
                cartCookieList.Add(new CartCookieModel() { UserName = userName, ShoppingCart = cart });
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
            var user = await signInManager.UserManager.GetUserAsync(httpContext.User);
            string? userName = user.UserName;
            if (!string.IsNullOrEmpty(userName))
            {
                var cookie = httpContext.Session.GetString("ShoppingCart");
                if (string.IsNullOrEmpty(cookie))
                {
                    return new()
                    {
                        User = userName
                    };
                }
                var cartCookieList = JsonConvert.DeserializeObject<List<CartCookieModel>>(cookie);
                if (cartCookieList == null)
                {
                    return new()
                    {

                        User = userName
                    };
                }
                var cartCookie = cartCookieList.FirstOrDefault(cc => cc.UserName == user.UserName);
                if (cartCookie == null)
                {
                    return new()
                    {
                        User = userName
                    };
                }
                return cartCookie.ShoppingCart;
            }
            return null;
        }
    }
}
