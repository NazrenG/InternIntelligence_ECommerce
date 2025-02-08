using ECommerce.Core.DataAccess;
using ECommerce.Entities.Models;

namespace ECommerce.DataAccess.Abstract
{
    public interface ICartItemRepository:IEntityRepository<CartItem>
    {
        Task<List<CartItem>> GetItems(int id);
    }
}
