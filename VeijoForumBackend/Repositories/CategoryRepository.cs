using Microsoft.EntityFrameworkCore;
using VeijoForumBackend.Data;
using VeijoForumBackend.Models;
using VeijoForumBackend.Repositories.Interfaces;

namespace VeijoForumBackend.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly VeijoForumBackendContext _context;

        public CategoryRepository(VeijoForumBackendContext context)
        {
            _context = context;
        }

        public void CreateCategory(Category category)
        {
            _context.Category.Add(category);
        }

        public async Task<List<Category>> GetCategories()
        {
            var categories = await _context.Category.ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Category.FindAsync(id);

            return category;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
