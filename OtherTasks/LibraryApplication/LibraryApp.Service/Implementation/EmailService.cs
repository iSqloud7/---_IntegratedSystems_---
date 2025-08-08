using LibraryApp.Domain.Email;
using LibraryApp.Service.Interface;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace LibraryApp.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            var email = new MimeMessage
            {
                Subject = emailMessage.Subject,
                Body = new TextPart(TextFormat.Plain)
                {
                    Text = emailMessage.Body
                },
                To =
                {
                    new MailboxAddress(emailMessage.SendTo, emailMessage.SendTo)
                }
            };

            try
            {
                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(_emailSettings.SmtpServer,
                    _emailSettings.SmtpPort);

                await smtp.AuthenticateAsync(_emailSettings.SmtpUser, _emailSettings.SmtpPassword);

                await smtp.SendAsync(email);

                await smtp.DisconnectAsync(true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
