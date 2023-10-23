using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.AuthDtos;
using VeijoForumBackend.Models.Auth;
using VeijoForumBackend.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using VeijoForumBackend.Models.Mail;
using VeijoForumBackend.Models.Dto;

namespace VeijoForumBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IMapper mapper, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<ResultResponse> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                return new ResultResponse
                {
                    Message = "Некорректный запрос",
                    IsSuccess = false
                };
            }

            var user = _mapper.Map<User>(registerUserDto);
            user.UserName = user.Email;

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (result.Succeeded)
            {
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var clientToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                var message = new Message(new List<string> { user.Email }, user.Email, "Email Confirmation");
                string url = $"https://localhost:7069/v1/auth/confirm?email={user.Email}&token={clientToken}";

                _mailService.SendConfirmEmailAsync(message, url);

                return new ResultResponse
                {
                    Message = "Успешная регистрация",
                    IsSuccess = true
                };
            }
            return new ResultResponse
            {
                Message = "Не удалось зарегистрировать пользователя",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        public async Task<Token> LoginUserAsync(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
            {
                return new Token
                {
                    Message = "Некорректный запрос",
                    IsSuccess = false
                };
            }

            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);

            if (user == null)
            {
                return new Token
                {
                    Message = "Пользователь не найден",
                    IsSuccess = false
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);

            if (result.Succeeded)
            {
                var token = await GenerateJwtToken(user);
                return token;
            }

            var errors = new List<string>();
            if (result.IsLockedOut)
            {
                errors.Add("Попытка авторизации заблокирована");
            }
            else if (result.IsNotAllowed)
            {
                errors.Add("Доступ заблокирован");
            }
            else if (result.RequiresTwoFactor)
            {
                errors.Add("Требуется двухфакторная аутентификация");
            }

            return new Token
            {
                Message = "Не удалось получить токен авторизации",
                IsSuccess = false,
                Errors = errors
            };
        }

        private async Task<Token> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AuthSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var accessToken = tokenHandler.WriteToken(token);
            var expiresIn = TimeSpan.FromSeconds(3600).TotalSeconds.ToString();


            var tokenModel = new Token
            {
                AccessToken = accessToken,
                ExpiresIn = expiresIn,
                TokenType = "Bearer",
                IsSuccess = true
            };

            return tokenModel;
        }

        public async Task<ResultResponse> ConfirmEmailAsync(ConfirmUserDto confirmUserDto)
        {
            var user = await _userManager.FindByEmailAsync(confirmUserDto.Email);

            if (user == null)
            {
                return new ResultResponse
                {
                    Message = "Пользователь не найден",
                    IsSuccess = false
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(confirmUserDto.Token);
            var serviceToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, serviceToken);

            if (result.Succeeded)
            {
                return new ResultResponse
                {
                    Message = "Сообщение успешно отправлено",
                    IsSuccess = true
                };
            }

            return new ResultResponse
            {
                Message = "Не удалось отправить сообщение",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        public async Task<ResultResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new ResultResponse
                {
                    Message = "Пользователь не найден",
                    IsSuccess = false
                };
            }

            var forgetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedEmailToken = Encoding.UTF8.GetBytes(forgetPasswordToken);
            var clientToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            var message = new Message(new List<string> { user.Email }, user.Email, "Email Confirmation");
            string url = $"https://localhost:7069/v1/auth/resetPassword?email={user.Email}&token={clientToken}";

            _mailService.SendForgetPasswordAsync(message, url);

            return new ResultResponse
            {
                Message = "Сообщение успешно отправлено",
                IsSuccess = true
            };

        }

        public async Task<ResultResponse> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (user == null)
            {
                return new ResultResponse
                {
                    Message = "Пользователь не найден",
                    IsSuccess = false
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(resetPasswordDto.Token);
            var serviceToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, serviceToken, resetPasswordDto.Password);

            if (result.Succeeded)
            {
                return new ResultResponse
                {
                    Message = "Пароль успешно изменен",
                    IsSuccess = true
                };
            }

            return new ResultResponse
            {
                Message = "Не удалось изменить пароль",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
    }
}
