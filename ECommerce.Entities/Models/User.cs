using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Models
{
    public class User:IdentityUser,  IEntity
    { 
        public virtual Cart? Cart { get; set; }
        public virtual List<Order>? Order { get; set; }
             
    }
}
