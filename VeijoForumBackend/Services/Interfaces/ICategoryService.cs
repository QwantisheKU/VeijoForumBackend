using VeijoForumBackend.Models.Dto.CategoryDtos;

namespace VeijoForumBackend.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<CategoryDto>> GetCategories();

        public Task<CategoryDto> GetCategoryById(int id);

        public bool CreateCategory(CreateCategoryDto category);
    }
}
