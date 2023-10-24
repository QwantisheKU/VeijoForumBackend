using VeijoForumBackend.Data;
using VeijoForumBackend.Models;
using VeijoForumBackend.Repositories.Interfaces;

namespace VeijoForumBackend.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly VeijoForumBackendContext _context;

        public ProfileRepository(VeijoForumBackendContext context)
        {
            _context = context;
        }

        public void CreateProfile(UserProfile userProfile)
        {
            _context.UserProfile.Add(userProfile);
        }

        public async Task<UserProfile> GetProfileByUserIdAsync(int userId)
        {
            var userProfile = _context.UserProfile.FirstOrDefault(x => x.UserId == userId);

            return userProfile;
        }

        // Add update functionality
        public void UpdateProfile(UserProfile userProfile)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
