using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VeijoForumBackend.Models.Dto.ProfileDtos;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Controllers
{
    [Route("v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public UsersController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{userId}/profile")]
        public async Task<ActionResult<ProfileDto>> GetUserProfile([Required] int userId)
        {
            var userProfile = await _profileService.GetProfileByUserIdAsync(userId);

            if (userProfile == null)
            {
                return NotFound();
            }

            return userProfile;
        }

        /*        [HttpPut("{id}")]
                public async Task<IActionResult> PutUserProfile(int id, UserProfile userProfile)
                {
                    if (id != userProfile.Id)
                    {
                        return BadRequest();
                    }

                    _context.Entry(userProfile).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserProfileExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return NoContent();
                }*/
    }
}
