using BusinessObjects;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impls
{
    public class CategoryService : ICategoryService
    {
        private IGenericRepository<Category> _categoryRepo;

        public CategoryService(IServiceProvider service)
        {
            _categoryRepo = service.GetRequiredService<IGenericRepository<Category>>();
        }


        public Category AddCategory(Category category)
        {
            try
            {
                _categoryRepo.InsertAsync(category);
                Category savedCategory = _categoryRepo.FistOrDefault(catego => catego.CategoryName.Equals(category.CategoryName)).Result;
                return savedCategory;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when adding category", ex);
            }
            
        }

        public void DeleteCategory(int id)
        {
            try
            {
                Category category = _categoryRepo.GetByIdAsync(id).Result;
                if(category == null)
                {
                    throw new Exception("Category not found");
                }
                _categoryRepo.DeleteAsync(category);
            }catch(Exception ex)
            {
                throw new Exception("Error when deleting category", ex);
            }
        }

        public List<Category> GetCategories()
        {
            return _categoryRepo.GetAllAsync().Result.ToList();
        }

        public Category GetCategoryById(int id)
        {
            Category category = _categoryRepo.GetByIdAsync(id).Result;
            if(category == null)
            {
                throw new Exception("Category not found");
            }
            return category;
        }

        public Category GetCategoryByName(string name)
        {
            Category category = _categoryRepo.FistOrDefault(catego => catego.CategoryName.Equals(name)).Result;
            return category;
        }

        public void UpdateCategory(Category category)
        {
            Category categoryToUpdate = _categoryRepo.GetByIdAsync(category.CategoryId).Result;
            if(categoryToUpdate == null)
            {
                throw new Exception("Category not found");
            }
            try
            {
                _categoryRepo.UpdateByIdAsync(category, category.CategoryId);
            }catch(Exception ex)
            {
                throw new Exception("Error when updating category", ex);
            }

        }
    }
}
