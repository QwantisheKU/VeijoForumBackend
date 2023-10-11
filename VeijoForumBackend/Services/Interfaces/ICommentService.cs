using VeijoForumBackend.Models.Dto.CommentDtos;

namespace VeijoForumBackend.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<List<CommentDto>> GetCommentsByParent(int parentId);

        public Task<CommentDto> GetCommentById(int id);

        public bool CreateComment(CreateCommentDto comment);

        public bool UpdateComment(CommentDto comment);

        public bool DeleteCommentById(int id);
    }
}
