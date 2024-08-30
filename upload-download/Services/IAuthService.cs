using upload_download.Models;

namespace upload_download.Services
{
    public interface IAuthService
    {
        Task<bool> SignUp(EmailPasswordModel request);
        Task<string> SignIn(EmailPasswordModel request);
    }
}