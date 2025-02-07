using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Models
{
    public class Category:IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual List<Product>? Products { get; set; }
    }
}
