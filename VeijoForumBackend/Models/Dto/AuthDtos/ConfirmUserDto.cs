using System.ComponentModel.DataAnnotations;

namespace VeijoForumBackend.Models.Dto.AuthDtos
{
    public class ConfirmUserDto
    {
        [Required(ErrorMessage = "Email обязателен для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный Email адрес")]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
