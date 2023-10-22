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

        public async Task<bool> RegisterUserAsync(RegisterUserDto registerUserDto)
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
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var clientToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                var message = new Message(new List<string> { user.Email }, user.Email, "Email Confirmation");
                string url = $"https://localhost:7069/v1/auth/confirm?email={user.Email}&token={clientToken}";

                _mailService.SendConfirmEmailAsync(message, url);

                return true;
            }
            return false;
        }

        public async Task<Token> LoginUserAsync(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
            {
                return null;
            }

            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);

            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);

            if (result.Succeeded)
            {
                var token = await GenerateJwtToken(user);
                return token;
            }

            return null;
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
                TokenType = "Bearer"
            };

            return tokenModel;
        }

        public async Task<bool> ConfirmEmailAsync(ConfirmUserDto confirmUserDto)
        {
            var user = await _userManager.FindByEmailAsync(confirmUserDto.Email);

            if (user == null)
            {
                return false;
            }

            var decodedToken = WebEncoders.Base64UrlDecode(confirmUserDto.Token);
            var serviceToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, serviceToken);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
