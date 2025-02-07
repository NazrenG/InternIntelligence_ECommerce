using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstarct
{
    public interface IProductService
    {
        public Task AddProduct(Product product);
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetProductById(int id);
        public Task DeleteProduct(int id);
        public Task UpdateProduct(Product product);
        public Task<Product> GetProductByCategoryId(int categoryId);
        

    }
}
