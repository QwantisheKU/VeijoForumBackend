namespace VeijoForumBackend.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string NickName { get; set; }

        public string? ImageUrl { get; set; }

        public string? BirthDate { get; set; }

        public string? Status { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
