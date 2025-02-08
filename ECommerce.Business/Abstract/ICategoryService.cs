using ECommerce.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstract
{
    public interface ICategoryService
    {
        public Task AddCategory(Category category);
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int id);
        public Task<int> GetCategoryByName(string name);
        public Task DeleteCategory(int id);
        public Task UpdateCategory(Category category);
    }
}
