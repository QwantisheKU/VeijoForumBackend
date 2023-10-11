using Microsoft.EntityFrameworkCore;
using VeijoForumBackend.Data;
using VeijoForumBackend.Models;
using VeijoForumBackend.Repositories.Interfaces;

namespace VeijoForumBackend.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly VeijoForumBackendContext _context;

        public CommentRepository(VeijoForumBackendContext context)
        {
            _context = context;
        }

        public void CreateComment(Comment comment)
        {
            _context.Comment.Add(comment);
        }

        public void DeleteCommentById(int id)
        {
            var comment = _context.Comment.Find(id);
            if (comment != null)
            {
                comment.Text = "Это сообщение было удалено";
                // TODO: Remove user
            }
        }

        public async Task<Comment> GetCommentById(int id)
        {
            var comment = await _context.Comment.FindAsync(id);

            return comment;
        }

        public async Task<List<Comment>> GetCommentsByParent(int parentId)
        {
            var comments = await _context.Comment.Where(x => x.ParentId == parentId).ToListAsync();

            return comments;
        }

        public void UpdateComment(Comment comment)
        {
            _context.Comment.Update(comment);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
