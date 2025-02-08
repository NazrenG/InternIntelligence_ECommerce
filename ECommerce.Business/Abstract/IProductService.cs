using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstract
{
    public interface IProductService
    {
        public Task AddProduct(Product product);
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetProductById(int id);
        public Task DeleteProduct(int id);
        public Task UpdateProduct(Product product);
        public Task<List<Product>> GetProductByCategoryId(int categoryId);

        public Task ChangeCount(bool check, int id);
    }
}
