using ECommerce.Core.DataAccess;
using ECommerce.Entities.Models;

namespace ECommerce.DataAccess.Abstract
{
    public interface IProductRepository : IEntityRepository<Product>
    {
    }
}
