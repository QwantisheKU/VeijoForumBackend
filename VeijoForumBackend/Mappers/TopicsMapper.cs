using AutoMapper;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.TopicDtos;

namespace VeijoForumBackend.Mappers
{
    public class TopicsMapper : Profile
    {
        public TopicsMapper()
        {
            CreateMap<Topic, TopicDto>().ReverseMap();
            CreateMap<Topic, CreateTopicDto>().ReverseMap();
        }
    }
}
