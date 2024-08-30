using System.Net;
using System.Net.Mail;
using System.Web;
using upload_download.Models;

namespace upload_download.Extensions
{
    public class EmailExtension
    {
        public static string GenerateEmailConfirmationLink(string token, string email)
        {
            // Encode the token and email to be safe for URL
            var encodedToken = HttpUtility.UrlEncode(token);
            var encodedEmail = HttpUtility.UrlEncode(email);

            // Create the confirmation link
            string domain = "http://localhost:5114";
            var confirmationLink = $"{domain}/api/email/confirm-email?token={encodedToken}&email={encodedEmail}";
            return confirmationLink;
        }

        public static async Task<bool> SendEmail(SMTPModel smtp, SendEmailModel model)
        {
            try
            {
                var smtpClient = new SmtpClient(smtp.Server)
                {
                    Port = int.Parse(smtp.Port),
                    Credentials = new NetworkCredential(smtp.Username, smtp.Password),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtp.Email, smtp.Name),
                    Subject = model.SubJect,
                    Body = model.Body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(model.ToEmail);
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}