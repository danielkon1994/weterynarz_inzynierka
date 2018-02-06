using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Weterynarz.Web.Services;

namespace Weterynarz.Web.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Potwierdzenie adresu e-mail",
                $"Potwierd� za�o�enie konta w aplikacji WetWebsite: <a href='{HtmlEncoder.Default.Encode(link)}'>Link</a>");
        }
    }
}
