using Microsoft.AspNetCore.Mvc;
using VeijoForumBackend.Models.Dto.AuthDtos;
using VeijoForumBackend.Models.Auth;
using VeijoForumBackend.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using VeijoForumBackend.Models.Dto;

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
        public async Task<ActionResult<ResultResponse>> RegisterUser(RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.RegisterUserAsync(registerUserDto);

            if (result.IsSuccess)
            {
                return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
            }

            // TODO: Add custom error model and show it instead of just bool
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Token>> LoginUser(LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.LoginUserAsync(loginUserDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpPost("confirm")]
        public async Task<ActionResult<ResultResponse>> ConfirmEmail([FromQuery] ConfirmUserDto confirmUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.ConfirmEmailAsync(confirmUserDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("forgetPassword")]
        public async Task<ActionResult<ResultResponse>> ForgetPassword([Required] string email)
        {
            var result = await _authService.ForgetPasswordAsync(email);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult<ResultResponse>> ResetPassword([FromQuery] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.ResetPasswordAsync(resetPasswordDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
