using System.ComponentModel.DataAnnotations;

namespace VeijoForumBackend.Models.Dto.CategoryDtos
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
