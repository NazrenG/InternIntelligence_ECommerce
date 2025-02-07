using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Models
{
    public class OrderItem:IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
        [Column(TypeName ="decimal(6,2)")]
        public double Price { get; set; }



        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }

    }
}
