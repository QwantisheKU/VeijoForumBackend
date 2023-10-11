using VeijoForumBackend.Models;

namespace VeijoForumBackend.Repositories.Interfaces
{
    public interface ITagRepository
    {
        public Task<List<Tag>> GetTags();

        public Task<Tag> GetTagById(int id);

        public void CreateTag(Tag tag);

        public void Save();
    }
}
