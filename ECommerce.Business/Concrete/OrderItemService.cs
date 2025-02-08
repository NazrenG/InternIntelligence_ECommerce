
using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Models;

namespace ECommerce.Business.Concrete
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task AddOrderItem(OrderItem orderItem)
        {
            await _orderItemRepository.Add(orderItem);
        }

        public async Task ChangeCount(bool check, int id)
        {
            var item = await _orderItemRepository.GetById(p => p.Id == id);
            if (item == null) 
            {
                throw new Exception("Order item not found!");
            }

            if (check) // true => increase
            {
                item.Count += 1;
            }
            else
            {
                if (item.Count > 0) 
                {
                    item.Count -= 1;
                }
            }

            await _orderItemRepository.Update(item);
        }


        public async Task DeleteOrderItem(int id)
        {
            var item = await _orderItemRepository.GetById(p => p.Id == id);
            await _orderItemRepository.Delete(item);
        }

        public async Task<List<OrderItem>> GetAllOrderItems()
        {
            return await _orderItemRepository.GetAll();
        }

        public async Task<List<OrderItem>> GetItemsByOrderId(int orderId)
        {
            return await _orderItemRepository.GetAll(p => p.OrderId == orderId);
        }

        public async Task<OrderItem> GetOrderItemById(int id)
        {
            return await _orderItemRepository.GetById(p => p.Id == id);
        }

        public async Task UpdateOrderItem(OrderItem orderItem)
        {
            await _orderItemRepository.Update(orderItem);
        }
    }
}
