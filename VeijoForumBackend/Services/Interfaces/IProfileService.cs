using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.ProfileDtos;

namespace VeijoForumBackend.Services.Interfaces
{
    public interface IProfileService
    {
        public Task<ProfileDto> GetProfileByUserIdAsync(int userId);

        public bool CreateProfile(CreateProfileDto profile);

        public bool UpdateProfile(UserProfile profile);
    }
}
