using System.ComponentModel.DataAnnotations;

namespace VeijoForumBackend.Models.Dto.AuthDtos
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Произошла непредвиденная ошибка")]
        public string Token { get; set; }

        [Required(ErrorMessage = "Email обязателен для заполнения")]
        [EmailAddress(ErrorMessage = "Некорректный Email адрес")]
        public string Email { get; set; }

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
