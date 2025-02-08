using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstract
{
    public interface IOrderService
    {
        public Task AddOrder(Order order);
        public Task<List<Order>> GetOrdersByUserId(string userId);
        public Task<List<Order>> GetAllOrders();//only admin panel
        public Task<Order> GetOrderById(int id);
        public Task DeleteOrder(int id);
        public Task UpdateOrder(Order order);
    }
}
