using System.ComponentModel.DataAnnotations;

namespace VeijoForumBackend.Models.Dto.TagDtos
{
    public class CreateTagDto
    {
        [Required]
        public string Name { get; set; }
    }
}
