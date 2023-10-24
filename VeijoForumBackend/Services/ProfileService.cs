using AutoMapper;
using VeijoForumBackend.Models;
using VeijoForumBackend.Models.Dto.ProfileDtos;
using VeijoForumBackend.Repositories.Interfaces;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public ProfileService(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public bool CreateProfile(CreateProfileDto createProfileDto)
        {
            if (createProfileDto == null)
            {
                return false;
            }

            var profile = _mapper.Map<UserProfile>(createProfileDto);

            try
            {
                _profileRepository.CreateProfile(profile);
                _profileRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProfileDto> GetProfileByUserIdAsync(int userId)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(userId);

            var mappedProfile = _mapper.Map<ProfileDto>(profile);

            return mappedProfile;
        }

        public bool UpdateProfile(UserProfile profile)
        {
            throw new NotImplementedException();
        }
    }
}
