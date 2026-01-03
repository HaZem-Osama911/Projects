using Microsoft.AspNetCore.Identity.UI.Services;
using MyApp.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace MyApp.Services.Implementations
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(
                    "hazemosama322@gmail.com", 
                    "kpliqrddwonlipsi"          
                ),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress("hazemosama322@gmail.com", "ASP.NET MyApp"), 
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mail.To.Add(toEmail);

            await smtp.SendMailAsync(mail);
        }
    }
}
