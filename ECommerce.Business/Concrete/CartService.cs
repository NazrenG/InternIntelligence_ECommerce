using ECommerce.Business.Abstarct;
using ECommerce.DataAccess.Abstarct;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
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
