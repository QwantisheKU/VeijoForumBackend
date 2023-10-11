using VeijoForumBackend.Models.Dto.TagDtos;
using System.ComponentModel.DataAnnotations;

namespace VeijoForumBackend.Models.Dto.TopicDtos
{
    public class CreateTopicDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int UserId { get; set; }

        public int? CategoryId { get; set; }

        public List<CreateTagDto>? Tags { get; set; }
    }
}
