﻿using ECommerce.Core.DataAccess;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.Abstarct
{
    public interface IOrderRepository : IEntityRepository<Order>
    {
        public Task<Order> GetOrderItems(string userId);
    }
}
