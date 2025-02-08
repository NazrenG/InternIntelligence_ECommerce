using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Abstract;
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

        public async Task<List<Product>> GetProductByCategoryId(int categoryId)
        {
          return await _repository.GetAll(p => p.CategoryId == categoryId);

        }

        public async Task UpdateProduct(Product product)
        {
          await _repository.Update(product);
        }

        public async Task ChangeCount(bool check, int id)
        {
            var item = await _repository.GetById(p => p.Id == id);
            if (item == null)
            {
                throw new Exception("Product not found!");
            }

            if (check) // true => increase
            {
                item.Count += 1;
            }
            else
            {
                if (item.Count > 0)
                {
                    item.Count -= 1;
                }
            }

            await _repository.Update(item);
        }
    }
}
