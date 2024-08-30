using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using upload_download.Models;

namespace upload_download.Extensions
{
    public class AppSettingsExtension
    {
        public static SMTPModel GetSMTPSettings(IConfiguration _configuration)
        {
            return new SMTPModel
            {
                Email = _configuration["SmtpSettings:SenderEmail"] ?? string.Empty,
                Name = _configuration["SmtpSettings:SenderName"] ?? string.Empty,
                Server = _configuration["SmtpSettings:Server"] ?? string.Empty,
                Password = _configuration["SmtpSettings:Password"] ?? string.Empty,
                Username = _configuration["SmtpSettings:Username"] ?? string.Empty,
                Port = _configuration["SmtpSettings:Port"] ?? string.Empty
            };
        }
    }
}