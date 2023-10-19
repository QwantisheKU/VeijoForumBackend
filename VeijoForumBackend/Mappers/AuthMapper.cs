using AutoMapper;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.AuthDtos;

namespace VeijoForumBackend.Mappers
{
    public class AuthMapper : Profile
    {
        public AuthMapper()
        {
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
