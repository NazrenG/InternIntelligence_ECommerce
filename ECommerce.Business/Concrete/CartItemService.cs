
using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
using ECommerce.Entities.Models;

namespace ECommerce.Business.Concrete
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository repository; 

        public CartItemService(ICartItemRepository repository)
        {
            this.repository = repository; 
        }

        public async Task AddCartItem(CartItem cartItem)
        {
            await repository.Add(cartItem);
        }

        public async Task DeleteCartItem(int id)
        {
            var item=await repository.GetById(p=>p.Id==id);
            await repository.Delete(item);
        }

        public async Task<List<CartItem>> GetAllCartItem()
        {
           return await repository.GetAll();    
        }

        public async Task<List<CartItem>> GetAllItemsForCartId(int cartId)
        { 
            return await repository.GetItems(cartId);
        }

        public async Task<CartItem> GetCartItemById(int id)
        {
       return await repository.GetById(p => p.Id == id);
        }

        public async Task UpdateCartItem(CartItem cartItem)
        {
            await repository.Update(cartItem);
        }
    }
}
