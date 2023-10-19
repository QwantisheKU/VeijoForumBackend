using System.ComponentModel.DataAnnotations;

namespace VeijoForumBackend.Models.Dto.AuthDtos
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "Email обязателен для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный Email адрес")]
        public string Email { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "Необходимо ввести никнейм длиной от 1 до 32 символов")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Необходимо ввести пароль длиной от 6 до 32 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Необходимо ввести пароль длиной от 6 до 32 символов")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
