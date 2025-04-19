using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using Application.Helper;
using Domain.IServices;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task<bool> SendEmail(string sendTo, string subject, string body)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_emailSettings.email),
                Subject = subject,
            };

            var builder = new BodyBuilder();

            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();

            email.To.Add(MailboxAddress.Parse(sendTo));
            email.From.Add(new MailboxAddress(_emailSettings.displayName, _emailSettings.email));

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_emailSettings.host, _emailSettings.port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailSettings.email, _emailSettings.password);
                await smtp.SendAsync(email);

                smtp.Disconnect(true);
            }

            return true;

        }
    }
}