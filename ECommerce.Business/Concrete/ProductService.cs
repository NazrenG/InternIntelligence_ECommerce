using ECommerce.Business.Abstarct;
using ECommerce.DataAccess.Abstarct;
using ECommerce.Entities.Models;

namespace ECommerce.Business.Concrete
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task AddProduct(Product product)
        {
            await _repository.Add(product);
        }

        public async Task DeleteProduct(int id)
        {
            await _repository.GetById(p => p.Id == id);
        }

        public async Task<List<Product>> GetAllProducts()
        {
           return await _repository.GetAll();   
        }

        public async Task<Product> GetProductById(int id)
        {
           return await _repository.GetById(p => p.Id == id);
        }

        public async Task<Product> GetProductByCategoryId(int categoryId)
        {
          return await _repository.GetById(p => p.CategoryId == categoryId);

        }

        public async Task UpdateProduct(Product product)
        {
          await _repository.Update(product);
        }
    }
}
