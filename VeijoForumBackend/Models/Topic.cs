namespace VeijoForumBackend.Models
{
    public class Topic
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public int UserId { get; set; }

        public Category? Category { get; set; }

        public int? CategoryId { get; set; }

        public List<Tag>? Tags { get; set; } = new List<Tag>();

        public List<Comment>? Comments { get; set; } = new List<Comment>();
    }
}
