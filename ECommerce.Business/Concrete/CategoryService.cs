using ECommerce.Business.Abstarct;
using ECommerce.DataAccess.Abstarct;
using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategory(Category category)
        {
           await _categoryRepository.Add(category);
        }

        public async Task DeleteCategory(int id)
        {
          var item= await _categoryRepository.GetById(p=>p.Id==id); 
            await _categoryRepository.Delete(item);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAll();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _categoryRepository.GetById(p => p.Id == id);
        }

        public async Task<int> GetCategoryByName(string name)
        {
            if (string.IsNullOrEmpty(name)) { throw new ArgumentException("Category name cannot be null or empty", nameof(name)); };
            var item=await _categoryRepository.GetById(predicate=>predicate.Name==name);    
          return item?.Id??0;  
        }

        public async Task UpdateCategory(Category category)
        {
           await _categoryRepository.Update(category);
        }
    }
}
