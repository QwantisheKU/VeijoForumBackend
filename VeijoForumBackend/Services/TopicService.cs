using AutoMapper;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto;
using VeijoForumBackend.Models.Dto.CommentDtos;
using VeijoForumBackend.Models.Dto.TopicDtos;
using VeijoForumBackend.Repositories;
using VeijoForumBackend.Repositories.Interfaces;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicsRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public TopicService(ITopicRepository topicsRepository, ITagRepository tagRepository, IMapper mapper, ICommentRepository commentRepository)
        {
            _topicsRepository = topicsRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public async Task<bool> CreateTopicAsync(CreateTopicDto createTopicDto)
        {
            if (createTopicDto == null)
            {
                return false;
            }

            var topic = _mapper.Map<Topic>(createTopicDto);

            // Maybe rework
            var tags = await _tagRepository.GetTags();
            if (tags != null && tags.Any() && topic != null && topic.Tags != null && topic.Tags.Any())
            {
                var finalTags = tags.Where(t => topic.Tags.Any(x => x.Name == t.Name)).ToList();
                topic.Tags = finalTags;
            }

            try
            {
                _topicsRepository.CreateTopic(topic);
                _topicsRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteTopicById(int id)
        {
            try
            {
                _topicsRepository.DeleteTopicById(id);
                _topicsRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<TopicDto> GetTopicByIdAsync(int id)
        {
            var topic = await _topicsRepository.GetTopicByIdAsync(id);

            var mappedTopic = _mapper.Map<TopicDto>(topic);
            if (mappedTopic != null && mappedTopic.Comments != null && mappedTopic.Comments.Any())
            {
                mappedTopic.Comments = AddTotalReplies(mappedTopic.Comments).Where(x => x.ParentId == null).ToList();
                mappedTopic.TotalReplies = mappedTopic.Comments?.Count;
            }

            return mappedTopic;
        }

        public async Task<List<TopicDto>> GetTopicsAsync(TopicQuery topicQuery)
        {
            // Fixing PageNumber in query
            topicQuery.PageNumber = topicQuery.PageNumber > 0 ? topicQuery.PageNumber : 1;

            var topics = await _topicsRepository.GetTopicsAsync(topicQuery);

            var mappedTopics = _mapper.Map<List<TopicDto>>(topics);

            return mappedTopics;
        }

        public bool UpdateTopic(TopicDto topicDto)
        {
            if (topicDto == null)
            {
                return false;
            }

            var topic = _mapper.Map<Topic>(topicDto);

            try
            {
                _topicsRepository.UpdateTopic(topic);
                _topicsRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Count number of comments and replies
        public List<CommentDto> AddTotalReplies(List<CommentDto> commentDtos)
        {
            commentDtos.ForEach(x => x.TotalReplies = _commentRepository.GetCommentsByParent(x.Id).Result.Count);

            return commentDtos;
        }
    }
}
