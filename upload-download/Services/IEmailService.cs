
using upload_download.Models;

namespace upload_download.Services
{
    public interface IEmailService
    {
        Task<bool> ConfirmEmail(string token, string email);
    }
}