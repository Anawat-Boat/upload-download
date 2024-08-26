using System.Net;
using System.Net.Mail;
using upload_download.Entity;
using upload_download.Extensions;
using upload_download.Models;
using upload_download.Repository;

namespace upload_download.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(SendEmailModel request)
        {
            var smtpClient = new SmtpClient(_configuration["SmtpSettings:Server"])
            {
                Port = int.Parse(_configuration["SmtpSettings:Port"]),
                Credentials = new NetworkCredential(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["SmtpSettings:SenderEmail"], _configuration["SmtpSettings:SenderName"]),
                Subject = request.SubJect,
                Body = request.Body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(request.ToEmail);
            await smtpClient.SendMailAsync(mailMessage);
            return true;
        }

        // public async Task<bool> SendEmail(SendEmailModel request)
        // {
        //     var smtpClient = new SmtpClient(_configuration["SmtpSettings:Server"])
        //     {
        //         Port = int.Parse(_configuration["SmtpSettings:Port"]),
        //         Credentials = new NetworkCredential(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]),
        //         EnableSsl = true,
        //     };

        //     var mailMessage = new MailMessage
        //     {
        //         From = new MailAddress(_configuration["SmtpSettings:SenderEmail"], _configuration["SmtpSettings:SenderName"]),
        //         Subject = request.SubJect,
        //         Body = request.Body,
        //         IsBodyHtml = true,
        //     };

        //     mailMessage.To.Add(request.ToEmail);
        //     await smtpClient.SendMailAsync(mailMessage);
        //     return true;
        // }
    }
}