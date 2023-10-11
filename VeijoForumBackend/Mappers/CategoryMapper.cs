using AutoMapper;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.CategoryDtos;

namespace VeijoForumBackend.Mappers
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
        }
    }
}
