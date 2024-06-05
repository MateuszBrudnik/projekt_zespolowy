using Projekt.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projekt.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task AddCategoryAsync(Category category);
    }
}
