using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.DataAccess.Abstarct;
using ECommerce.Entities.Data;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Concrete
{
    public class CategoryRepository : EFEntityBaseRepository<ECommerceDbContext, Category>, ICategoryRepository
    {
        public CategoryRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
