using Demo.Helpers.Email;
using Demo.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace Demo.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailSenderService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public async Task SendEmailAsync(EmailMessage message)
        {
            var emaiMessage = CreateEmailMessage(message);
            await Send(emaiMessage);

        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("DemoECommerce",_emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Body};

            return emailMessage;
        }
        private async Task Send(MimeMessage mail)
        {
            using(var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfiguration.Username, _emailConfiguration.Password);
                    await client.SendAsync(mail);
                }catch
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
