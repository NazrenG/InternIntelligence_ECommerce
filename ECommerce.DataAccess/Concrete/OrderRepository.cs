using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Data;
using ECommerce.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Concrete
{
    public class OrderRepository : EFEntityBaseRepository<ECommerceDbContext, Order>,IOrderRepository
    {
        private readonly ECommerceDbContext _context;
        public OrderRepository(ECommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrderItems(string userId)
        {
            return await _context.Orders.Include(oi => oi.Items).Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
