using ECommerce.Core.DataAccess;
using ECommerce.Entities.Models;

namespace ECommerce.DataAccess.Abstract
{
    public interface ICartRepository:IEntityRepository<Cart>
    {
        public Task<Cart> GetUserCart(string userId);
    }
}
