using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstarct
{
    public interface IOrderItemService
    {
        public Task AddOrderItem(OrderItem orderItem);
        public Task<List<OrderItem>> GetItemsByOrderId(int orderId);
        public Task<List<OrderItem>> GetAllOrderItems();//only admin panel
        public Task<OrderItem> GetOrderItemById(int id);
        public Task DeleteOrderItem(int id);
        public Task UpdateOrderItem(OrderItem orderItem);
        public Task ChangeCount(bool check,int id);
    }
}
