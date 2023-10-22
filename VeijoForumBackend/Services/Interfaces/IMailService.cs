using VeijoForumBackend.Models.Mail;

namespace VeijoForumBackend.Services.Interfaces
{
    public interface IMailService
    {
        public void SendConfirmEmailAsync(Message message, string url);
    }
}
