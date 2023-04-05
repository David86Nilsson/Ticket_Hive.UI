using Microsoft.EntityFrameworkCore;
using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public class ShoppingCartModelRepo : IShoppingCartModelRepo
    {
        private readonly EventDbContext context;

        public ShoppingCartModelRepo(EventDbContext context)
        {
            this.context = context;
        }
        public async Task AddShoppingCartAsync(ShoppingCartModel newShoppingCart)
        {
            await context.ShoppingCarts.AddAsync(newShoppingCart);
            await context.SaveChangesAsync();
        }

        public async Task DeleteShoppingCartAsync(ShoppingCartModel shoppingCartToDelete)
        {
            ShoppingCartModel? cart = await context.ShoppingCarts.FirstOrDefaultAsync(sc => sc.Id == shoppingCartToDelete.Id);
            if (cart != null)
            {
                context.ShoppingCarts.Remove(cart);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ShoppingCartModel>?> GetAllShoppingCartsAsync()
        {
            return await context.ShoppingCarts.ToListAsync();
        }

        public async Task<ShoppingCartModel?> GetShoppingCartByIdAsync(int id)
        {
            return await context.ShoppingCarts.FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<bool> UpdateShoppingCartAsync(ShoppingCartModel updatedShoppingCart)
        {
            ShoppingCartModel? cart = await context.ShoppingCarts.FirstOrDefaultAsync(sc => sc.Id == updatedShoppingCart.Id);
            if (cart != null)
            {
                cart.User = updatedShoppingCart.User;
                cart.Bookings = updatedShoppingCart.Bookings;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<ShoppingCartModel?> GetMostRecentShoppingCartAsync()
        {
            return await context.ShoppingCarts.OrderByDescending(sc => sc.Id).FirstOrDefaultAsync();
        }
    }
}
