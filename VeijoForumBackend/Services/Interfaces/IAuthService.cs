using VeijoForumBackend.Models.Dto.AuthDtos;
using VeijoForumBackend.Models.Auth;
using VeijoForumBackend.Models.Dto;

namespace VeijoForumBackend.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ResultResponse> RegisterUserAsync(RegisterUserDto registerUserDto);

        public Task<Token> LoginUserAsync(LoginUserDto loginUserDto);

        public Task<ResultResponse> ConfirmEmailAsync(ConfirmUserDto confirmUserDto);

        public Task<ResultResponse> ForgetPasswordAsync(string email);

        public Task<ResultResponse> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    }
}
