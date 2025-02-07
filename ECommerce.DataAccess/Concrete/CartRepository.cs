using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.DataAccess.Abstarct;
using ECommerce.Entities.Data;
using ECommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Concrete
{
    public class CartRepository : EFEntityBaseRepository<ECommerceDbContext, Cart>, ICartRepository
    {
        private readonly ECommerceDbContext _context;   
        public CartRepository(ECommerceDbContext context) : base(context)
        {
            _context = context;
        } 
        public async Task<Cart> GetUserCart(string userId)
        {
            return await _context.Carts.Include(c => c.CartItems).ThenInclude(ci =>ci.Product)
                 .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
