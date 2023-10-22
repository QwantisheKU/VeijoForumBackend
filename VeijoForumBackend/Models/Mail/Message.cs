using MimeKit;

namespace VeijoForumBackend.Models.Mail
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(List<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("Test", x)));
            Subject = subject;
            Content = content;
        }
    }
}
