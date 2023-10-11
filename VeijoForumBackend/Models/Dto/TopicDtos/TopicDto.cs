using VeijoForumBackend.Models.Dto.CategoryDtos;
using VeijoForumBackend.Models.Dto.CommentDtos;
using VeijoForumBackend.Models.Dto.TagDtos;

namespace VeijoForumBackend.Models.Dto.TopicDtos
{
    public class TopicDto
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateUpdated { get; set; } = DateTime.Now;

        public CategoryDto? Category { get; set; }

        public List<TagDto>? Tags { get; set; } = new List<TagDto>();

        public List<CommentDto>? Comments { get; set; } = new List<CommentDto>();

        public int? TotalReplies { get; set; }
    }
}
