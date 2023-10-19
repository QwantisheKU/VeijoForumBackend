using VeijoForumBackend.Models.Dto.AuthDtos;

namespace VeijoForumBackend.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> RegisterUser(RegisterUserDto registerUserDto);
    }
}
