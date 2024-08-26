
using upload_download.Models;

namespace upload_download.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(SendEmailModel request);
    }
}