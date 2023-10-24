namespace VeijoForumBackend.Models
{
    public class Reaction
    {
        public int Id { get; set; }

        public ReactionType ReactionType { get; set; }

        public int EntityId { get; set; }

        public EntityType EntityType { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }

    public enum ReactionType
    {
        Like,
        Smile,
        Fire
    }

    public enum EntityType
    {
        Topic,
        Comment
    }
}
