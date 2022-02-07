using System.Net.Mail;
using AngularEshop.Core.Services.Interfaces;

namespace AngularEshop.Core.Services.Implementations
{
    public class SendEmail : IMailSender
    {
        public void Send(string to, string subject, string body)
        {
            var defaultEmail = "ayobkafrashian@gmail.com";

            var mail = new MailMessage();

            var SmtpServer = new SmtpClient("ayobkafrashian@gmail.com");

            mail.From = new MailAddress(defaultEmail, "فروشگاه انگولار");

            mail.To.Add(to);

            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;

            // System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;

            SmtpServer.Credentials = new System.Net.NetworkCredential(defaultEmail, "9016225061");

            SmtpServer.EnableSsl = false;

            SmtpServer.Send(mail);
        }
    }
}

