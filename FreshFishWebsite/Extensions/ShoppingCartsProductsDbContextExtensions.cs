using FreshFishWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FreshFishWebsite.Extensions
{
    public static class ShoppingCartsProductsDbContextExtensions
    {
        public static async Task<ShoppingCartProduct> GetShoppingCartProductById(this DbSet<ShoppingCartProduct> shoppingCartProducts, int id)
        {
            return await shoppingCartProducts.FirstOrDefaultAsync(x => x.Id == id);
        }
        public static async Task<ShoppingCartProduct> GetShoppingCartProductWithProductById(this DbSet<ShoppingCartProduct> shoppingCartProducts, int id)
        {
            return await shoppingCartProducts
            .Include(p => p.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
