using AutoMapper;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.CommentDtos;

namespace VeijoForumBackend.Mappers
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CreateCommentDto>().ReverseMap();
        }
    }
}
