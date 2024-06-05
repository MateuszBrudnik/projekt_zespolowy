using Projekt.Entities;
using Projekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projekt.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ExpenseContext _context;

        public CategoryService(ExpenseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
    }
}
