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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
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
    }
}
