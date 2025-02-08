using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Data;
using ECommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Concrete
{
    public class CartItemRepository : EFEntityBaseRepository<ECommerceDbContext, CartItem>, ICartItemRepository
    {
        private readonly ECommerceDbContext _context;   
        public CartItemRepository(ECommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CartItem>> GetItems(int id)
        {
     return await _context.CartItems.Include(c => c.Cart).Where(ci => ci.Id == id).ToListAsync();
        }
    }
}
