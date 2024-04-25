using boredBets.Models.Dtos;
using boredBets.Repositories.Interface;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Net;
using System.Net.Mail;

namespace boredBets.Repositories
{
    public class EmailService : IEmailInterface
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration config)
        {
            this.config = config;
        }

        public void SendEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(config.GetSection
                ("EmailSettings:EmailUserName").Value));

            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect(config.GetSection("EmailSettings:EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(new NetworkCredential(config.GetSection("EmailSettings:EmailUserName").Value, config.GetSection("EmailSettings:EmailPassword").Value));

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}
