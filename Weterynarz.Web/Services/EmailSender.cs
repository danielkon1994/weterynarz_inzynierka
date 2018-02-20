using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Threading.Tasks;
using Weterynarz.Web.Models.Emails;

namespace Weterynarz.Web.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public EmailSettings _emailSettings { get; set; }

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendContactMessageAsync(string email, string subject, string message)
        {
            ExecuteContact(email, subject, message).Wait();
            return Task.CompletedTask;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            Execute(email, subject, message).Wait();
            return Task.CompletedTask;
        }

        public async Task Execute(string toEmail, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailSettings.FromEmail));
            mimeMessage.To.Add(new MailboxAddress(toEmail));
            mimeMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_emailSettings.PrimaryDomain);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);

                client.Send(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }

        public async Task ExecuteContact(string fromEmail, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(fromEmail));
            mimeMessage.To.Add(new MailboxAddress(_emailSettings.FromEmail));
            mimeMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_emailSettings.PrimaryDomain);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);

                client.Send(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
