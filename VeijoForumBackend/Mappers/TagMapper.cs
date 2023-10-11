using AutoMapper;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.TagDtos;

namespace VeijoForumBackend.Mappers
{
    public class TagMapper : Profile
    {
        public TagMapper()
        {
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<Tag, CreateTagDto>().ReverseMap();
        }
    }
}
