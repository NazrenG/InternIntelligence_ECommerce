using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Data;
using ECommerce.Entities.Models;

namespace ECommerce.DataAccess.Concrete
{
    public class ProductRepository : EFEntityBaseRepository<ECommerceDbContext, Product>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
