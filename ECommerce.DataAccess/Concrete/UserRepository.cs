using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Data;
using ECommerce.Entities.Models;

namespace ECommerce.DataAccess.Concrete
{
    public class UserRepository : EFEntityBaseRepository<ECommerceDbContext, User>, IUserRepository
    {
        public UserRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
