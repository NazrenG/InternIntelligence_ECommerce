using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Models
{
    public class CartItem:IEntity
    {
        public int Id { get; set; }
        public int CartId {  get; set; }
        public int ProductId {  get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual Product? Product { get; set; }
    }
}
