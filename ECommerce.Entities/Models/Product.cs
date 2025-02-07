using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }


        public int CategoryId { get; set; }
        public virtual Category? Category  { get; set; }
        public virtual List<OrderItem>? Items { get; set; }
        public virtual List<CartItem>? CartItems { get; set; }
    }
}
