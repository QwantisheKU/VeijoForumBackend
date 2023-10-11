using AutoMapper;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.CommentDtos;
using VeijoForumBackend.Repositories.Interfaces;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public bool CreateComment(CreateCommentDto createCommentDto)
        {
            if (createCommentDto == null)
            {
                return false;
            }

            var comment = _mapper.Map<Comment>(createCommentDto);

            try
            {
                _commentRepository.CreateComment(comment);
                _commentRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCommentById(int id)
        {
            try
            {
                _commentRepository.DeleteCommentById(id);
                _commentRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CommentDto> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentById(id);

            var mappedComment = _mapper.Map<CommentDto>(comment);

            return mappedComment;
        }

        public async Task<List<CommentDto>> GetCommentsByParent(int parentId)
        {
            var comments = await _commentRepository.GetCommentsByParent(parentId);

            var mappedComments = _mapper.Map<List<CommentDto>>(comments);

            return mappedComments;
        }

        public bool UpdateComment(CommentDto commentDto)
        {
            if (commentDto == null)
            {
                return false;
            }

            var comment = _mapper.Map<Comment>(commentDto);

            try
            {
                _commentRepository.UpdateComment(comment);
                _commentRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
