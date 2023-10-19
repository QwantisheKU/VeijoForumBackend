using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.AuthDtos;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> RegisterUser(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                return false;
            }

            var user = _mapper.Map<User>(registerUserDto);
            user.UserName = user.Email;

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            //var mappedUser = _mapper.Map<UserDto>(user);

            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
    }
}
