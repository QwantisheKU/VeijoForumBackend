using Microsoft.AspNetCore.Mvc;
using VeijoForumBackend.Models.Dto.AuthDtos;
using VeijoForumBackend.Models.Auth;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Controllers
{
    [Route("v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMailService _mailService;

        public AuthController(IAuthService authService, IMailService mailService)
        {
            _authService = authService;
            _mailService = mailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<bool>> RegisterUser(RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.RegisterUserAsync(registerUserDto);

            if (result)
            {
                return new ObjectResult(true) { StatusCode = StatusCodes.Status201Created };
            }

            // TODO: Add custom error model and show it instead of just bool
            return BadRequest(false);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Token>> LoginUser(LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
            {
                return BadRequest();
            }

            var result = await _authService.LoginUserAsync(loginUserDto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("confirm")]
        public async Task<ActionResult<bool>> ConfirmEmail([FromQuery] ConfirmUserDto confirmUserDto)
        {
            if (confirmUserDto == null)
            {
                return NotFound();
            }

            var result = await _authService.ConfirmEmailAsync(confirmUserDto);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
