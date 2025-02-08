
using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Models;

namespace ECommerce.Business.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task AddCart(Cart cart)
        {
        await _cartRepository.Add(cart);
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
          return await _cartRepository.GetUserCart(userId);
        }

        public async Task UpdateCart(Cart cart)
        {
          await _cartRepository.Update(cart);
        }
    }
}
