namespace VeijoForumBackend.Models.Auth
{
    public class Token
    {
        public string AccessToken { get; set; }

        public string ExpiresIn { get; set; }

        public string TokenType { get; set; }
    }
}
