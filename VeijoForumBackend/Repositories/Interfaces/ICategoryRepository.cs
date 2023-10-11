using VeijoForumBackend.Models;

namespace VeijoForumBackend.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetCategories();

        public Task<Category> GetCategoryById(int id);

        public void CreateCategory(Category category);

        public void Save();
    }
}
