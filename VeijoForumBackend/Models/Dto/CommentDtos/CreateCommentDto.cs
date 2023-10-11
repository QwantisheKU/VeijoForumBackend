namespace VeijoForumBackend.Models.Dto.CommentDtos
{
    public class CreateCommentDto
    {
        public string Text { get; set; }

        public int TopicId { get; set; }

        public int? ParentId { get; set; }
    }
}
