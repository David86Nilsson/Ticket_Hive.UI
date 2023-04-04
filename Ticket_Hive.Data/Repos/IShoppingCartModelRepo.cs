using Ticket_Hive.Data.Models;

namespace Ticket_Hive.Data.Repos
{
    public interface IShoppingCartModelRepo
    {
        public Task<IEnumerable<ShoppingCartModel>> GetAllShoppingCartsAsync();
        public Task<ShoppingCartModel?> GetShoppingCartByIdAsync(int id);
        public Task AddShoppingCartAsync(ShoppingCartModel newShoppingCart);
        public Task<bool> UpdateShoppingCartAsync(ShoppingCartModel updatedShoppingCart);
        public Task DeleteShoppingCartAsync(ShoppingCartModel shoppingCartToDelete);
    }
}
