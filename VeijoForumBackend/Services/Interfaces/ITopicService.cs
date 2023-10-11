using VeijoForumBackend.Models.Dto;
using VeijoForumBackend.Models.Dto.CommentDtos;
using VeijoForumBackend.Models.Dto.TopicDtos;

namespace VeijoForumBackend.Services.Interfaces
{
    public interface ITopicService
    {
        public Task<List<TopicDto>> GetTopicsAsync(TopicQuery topicQuery);

        public Task<TopicDto> GetTopicByIdAsync(int id);

        public Task<bool> CreateTopicAsync(CreateTopicDto createtTopicDto);

        public bool UpdateTopic(TopicDto topic);

        public bool DeleteTopicById(int id);

        public List<CommentDto> AddTotalReplies(List<CommentDto> commentDtos);
    }
}
