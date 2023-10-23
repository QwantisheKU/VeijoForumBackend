using MailKit.Net.Smtp;
using MimeKit;
using VeijoForumBackend.Models.Mail;
using VeijoForumBackend.Services.Interfaces;

namespace VeijoForumBackend.Services
{
    public class MailService : IMailService
    {
        private readonly EmailConfiguration _emailConfig;

        public MailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendConfirmEmailAsync(Message message, string url)
        {
            var emailMessage = CreateConfirmEmailMessage(message, url);

            Send(emailMessage);
        }

        public void SendForgetPasswordAsync(Message message, string url)
        {
            var emailMessage = CreateForgetPasswordEmailMessage(message, url);

            Send(emailMessage);
        }

        private MimeMessage CreateConfirmEmailMessage(Message message, string url)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var html = File.ReadAllText(@"./Models/Mail/ConfirmEmail.html");
            html = html.Replace("{{link}}", url);
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = html };
            return emailMessage;
        }

        private MimeMessage CreateForgetPasswordEmailMessage(Message message, string url)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var html = File.ReadAllText(@"./Models/Mail/ResetPasswordEmail.html");
            html = html.Replace("{{link}}", url);
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = html };
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    // TODO: Logger
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
