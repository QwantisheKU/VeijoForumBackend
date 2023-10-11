namespace VeijoForumBackend.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public Topic Topic { get; set; }

        public int TopicId { get; set; }

        public int? ParentId { get; set; }
    }
}
