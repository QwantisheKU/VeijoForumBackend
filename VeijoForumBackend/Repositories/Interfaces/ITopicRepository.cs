using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto;

namespace VeijoForumBackend.Repositories
{
    public interface ITopicRepository
    {
        public Task<List<Topic>> GetTopicsAsync(TopicQuery topicQuery);

        public Task<Topic> GetTopicByIdAsync(int id);

        public void CreateTopic(Topic topic);

        public void UpdateTopic(Topic topic);

        public void DeleteTopicById(int id);

        public void Save();
    }
}
