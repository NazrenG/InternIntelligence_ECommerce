using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Models
{
    public class Order:IEntity
    {
        public int Id { get; set; }
        public string? Status { get; set; }//pending,ready

        public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual List<OrderItem>? Items { get;  set; }
    }
}
