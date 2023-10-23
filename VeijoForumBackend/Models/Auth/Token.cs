using VeijoForumBackend.Models.Dto;

namespace VeijoForumBackend.Models.Auth
{
    public class Token : ResultResponse
    {
        public string AccessToken { get; set; }

        public string ExpiresIn { get; set; }

        public string TokenType { get; set; }
    }
}
