using ECommerce.Core.DataAccess;
using ECommerce.Entities.Models;

namespace ECommerce.DataAccess.Abstract
{
    public interface IOrderRepository : IEntityRepository<Order>
    {
        public Task<List<Order>> GetOrderItems(string userId);
    }
}
