using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstract
{
    public interface ICartService
    {
        //Each user has one cart (i.e. basket), so baskets cannot be  deleted.
        //  UpdateCart=>If you want to update the total price or status of your cart

        public Task<Cart> GetCartByUserId(string userId);
        public Task UpdateCart(Cart cart); 
        public Task AddCart(Cart cart); 
    }
}
