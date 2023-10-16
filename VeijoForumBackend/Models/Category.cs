namespace VeijoForumBackend.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public List<Topic>? Topics { get; set; }
    }
}
