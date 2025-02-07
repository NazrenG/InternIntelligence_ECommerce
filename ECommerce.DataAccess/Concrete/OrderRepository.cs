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
    public class OrderRepository : EFEntityBaseRepository<ECommerceDbContext, Order>,IOrderRepository
    {
        private readonly ECommerceDbContext _context;
        public OrderRepository(ECommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderItems(string userId)
        {
            return await _context.Orders.Include(oi => oi.Items).FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
