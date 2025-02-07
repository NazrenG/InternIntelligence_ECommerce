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
    public class CartItemRepository : EFEntityBaseRepository<ECommerceDbContext, CartItem>, ICartItemRepository
    {
        public CartItemRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
