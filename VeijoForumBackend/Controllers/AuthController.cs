using Microsoft.AspNetCore.Mvc;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.AuthDtos;
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
        public async Task<ActionResult<User>> RegisterUser(RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _authService.RegisterUser(registerUserDto);


            if (result)
            {
                return new ObjectResult(true) { StatusCode = StatusCodes.Status201Created };
            }

            // TODO: Add custom error model and show it instead of just bool
            return BadRequest(false);
        }
    }
}
