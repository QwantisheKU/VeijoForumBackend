namespace VeijoForumBackend.Models.Dto.CommentDtos
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public int TopicId { get; set; }

        public int? ParentId { get; set; }

        public int TotalReplies { get; set; }
    }
}
