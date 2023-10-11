using AutoMapper;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.CategoryDtos;
using VeijoForumBackend.Repositories.Interfaces;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public bool CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null)
            {
                return false;
            }

            var category = _mapper.Map<Category>(createCategoryDto);

            try
            {
                _categoryRepository.CreateCategory(category);
                _categoryRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();

            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);

            return mappedCategories;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryById(id);

            var mappedCategory = _mapper.Map<CategoryDto>(category);

            return mappedCategory;
        }
    }
}
