using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.DataAccess.Abstarct;
using ECommerce.Entities.Data;
using ECommerce.Entities.Models;

namespace ECommerce.DataAccess.Concrete
{
    public class OrderItemRepository : EFEntityBaseRepository<ECommerceDbContext, OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
