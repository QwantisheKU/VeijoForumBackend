using Microsoft.EntityFrameworkCore;
using VeijoForumBackend.Data;
using VeijoForumBackend.Models;
using VeijoForumBackend.Repositories.Interfaces;

namespace VeijoForumBackend.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly VeijoForumBackendContext _context;

        public TagRepository(VeijoForumBackendContext context)
        {
            _context = context;
        }

        public void CreateTag(Tag tag)
        {
            _context.Tag.Add(tag);
        }

        public async Task<Tag> GetTagById(int id)
        {
            var tag = await _context.Tag.FindAsync(id);

            return tag;
        }

        public async Task<List<Tag>> GetTags()
        {
            var tags = await _context.Tag.ToListAsync();

            return tags;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
