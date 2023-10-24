using AutoMapper;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.ProfileDtos;

namespace VeijoForumBackend.Mappers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<UserProfile, ProfileDto>().ReverseMap();
            CreateMap<UserProfile, CreateProfileDto>().ReverseMap();
        }
    }
}
