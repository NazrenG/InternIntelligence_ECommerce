
using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Models;

namespace ECommerce.Business.Concrete
{
    public class OrderService:IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task AddOrder(Order order)
        {
            await _orderRepository.Add(order);
        }

        public async Task DeleteOrder(int id)
        {
            await _orderRepository.GetById(p => p.Id == id);
        }

        public async Task<List<Order>> GetAllOrders()
        {
           return await _orderRepository.GetAll();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _orderRepository.GetById(p => p.Id == id);
        }

        public async Task<List<Order>> GetOrdersByUserId(string userId)
        {
           return await _orderRepository.GetOrderItems(userId);
        }

        public async Task UpdateOrder(Order order)
        {
            await _orderRepository.Update(order);
        }
    }
}
