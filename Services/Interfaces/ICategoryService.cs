using BusinessObjects;
using BusinessObjects.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category GetCategoryById(int id);
        Category GetCategoryByName(string name);
        Category AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
    }
}
