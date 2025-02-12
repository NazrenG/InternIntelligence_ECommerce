﻿using ECommerce.Core.DataAccess.EntityFramework;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Data;
using ECommerce.Entities.Models;

namespace ECommerce.DataAccess.Concrete
{
    public class CategoryRepository : EFEntityBaseRepository<ECommerceDbContext, Category>, ICategoryRepository
    {
        public CategoryRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
