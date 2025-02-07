using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstarct
{
    public interface ICartItemService
    {
        public Task AddCartItem(CartItem cartItem);
        public Task<CartItem> GetCartItemById(int id);
        public Task DeleteCartItem(int id);
        public Task UpdateCartItem(CartItem cartItem);

        public Task<List<CartItem>> GetAllCartItem();//only admin panel
        public Task<List<CartItem>> GetAllItemsForCartId(int cartId);

    } }
