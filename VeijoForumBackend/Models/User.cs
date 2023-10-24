using Microsoft.AspNetCore.Identity;

namespace VeijoForumBackend.Models
{
    public class User : IdentityUser<int>
    {
        public List<UserRole> UserRoles { get; set; }

        public UserProfile? Profile { get; set; }

        public List<Reaction>? Reactions { get; set; }
    }
}
