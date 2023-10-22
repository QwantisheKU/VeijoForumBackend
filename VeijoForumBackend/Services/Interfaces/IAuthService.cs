using VeijoForumBackend.Models.Dto.AuthDtos;
using VeijoForumBackend.Models.Auth;

namespace VeijoForumBackend.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> RegisterUserAsync(RegisterUserDto registerUserDto);

        public Task<Token> LoginUserAsync(LoginUserDto loginUserDto);

        public Task<bool> ConfirmEmailAsync(ConfirmUserDto confirmUserDto);
    }
}
