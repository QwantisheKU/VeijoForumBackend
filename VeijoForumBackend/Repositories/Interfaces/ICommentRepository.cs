using VeijoForumBackend.Models;

namespace VeijoForumBackend.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetCommentsByParent(int parentId);

        public Task<Comment> GetCommentById(int id);

        public void CreateComment(Comment comment);

        public void UpdateComment(Comment comment);

        public void DeleteCommentById(int id);

        public void Save();
    }
}
