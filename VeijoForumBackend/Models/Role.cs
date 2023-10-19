using Microsoft.AspNetCore.Identity;

namespace VeijoForumBackend.Models
{
    public class Role : IdentityRole<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}
