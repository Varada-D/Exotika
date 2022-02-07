using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExotikaTrial2.Utility
{
    public class EmailSender : IEmailSender
    {
        // Send email using the smtp server
        // Emails can also be sent using third-party service providers (most popular: sendGrid)
        // However, sendGrid would not work with gmail/yahoo/hotmail... it requires a domain email (eg: hello@dotnetmastery.com
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("test@example.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            // send email using smtp client
            using (var emailClient = new SmtpClient())
            {
                // Establish connection to smtp server
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls); // smtp for google, default port number for google is 587, security
                // email id, pwd for authentication
                emailClient.Authenticate("exotikaindia@gmail.com", "Exotika2823@BW");
                // send the mail
                emailClient.Send(emailToSend);
                // it is always a good idea to disconnect your email client after this is done
                emailClient.Disconnect(true);
            }

            return Task.CompletedTask;
        }
    }
}
