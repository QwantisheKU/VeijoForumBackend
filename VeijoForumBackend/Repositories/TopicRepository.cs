using Microsoft.EntityFrameworkCore;
using VeijoForumBackend.Data;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto;

namespace VeijoForumBackend.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private readonly VeijoForumBackendContext _context;

        public TopicRepository(VeijoForumBackendContext context)
        {
            _context = context;
        }

        public void CreateTopic(Topic topic)
        {
            _context.Topic.Add(topic);
        }

        public void DeleteTopicById(int id)
        {
            var topic = _context.Topic.Find(id);
            if (topic != null)
            {
                _context.Topic.Remove(topic);
            }
        }

        public async Task<Topic> GetTopicByIdAsync(int id)
        {
            var topic = await _context.Topic
                .Include(c => c.Comments)
                .Include(t => t.Tags)
                .Include(ct => ct.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            return topic;
        }

        public async Task<List<Topic>> GetTopicsAsync(TopicQuery topicQuery)
        {
            var topics = await _context.Topic.Take(10).Skip(10 * (topicQuery.PageNumber - 1))
                .Include(c => c.Category)
                .Include(t => t.Tags)
                .ToListAsync();

            return topics;
        }

        public void UpdateTopic(Topic topic)
        {
            _context.Topic.Add(topic);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
