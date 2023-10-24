using VeijoForumBackend.Models;

namespace VeijoForumBackend.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        // TODO: Generic for repositories and services
        public Task<UserProfile> GetProfileByUserIdAsync(int userId);

        public void CreateProfile(UserProfile profile);

        public void UpdateProfile(UserProfile profile);

        public void Save();
    }
}
