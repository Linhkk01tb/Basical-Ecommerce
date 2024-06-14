using MimeKit;
using System.Net.Mail;

namespace Demo.Helpers.Email
{
    public class EmailMessage
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailMessage(IEnumerable<string> to, string subject, string body)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(s => new MailboxAddress(s,s)));
            Subject = subject;
            Body = body;
        }
    }
}
