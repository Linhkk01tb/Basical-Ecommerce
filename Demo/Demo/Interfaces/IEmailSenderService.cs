using Demo.Helpers.Email;

namespace Demo.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
