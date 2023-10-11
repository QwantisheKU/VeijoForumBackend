using Microsoft.EntityFrameworkCore;
using VeijoForumBackend.Models;

namespace VeijoForumBackend.Data
{
    public class VeijoForumBackendContext : DbContext
    {
        public VeijoForumBackendContext(DbContextOptions<VeijoForumBackendContext> options)
            : base(options)
        {
        }

        public DbSet<Topic> Topic { get; set; } = default!;

        public DbSet<Category> Category { get; set; } = default!;

        public DbSet<Tag> Tag { get; set; } = default!;

        public DbSet<Comment> Comment { get; set; } = default!;
    }
}
