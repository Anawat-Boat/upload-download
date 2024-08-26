using upload_download.Models;

namespace upload_download.Services
{
    public interface IAuthService
    {
        bool SignUp(SignUpModel request);
        string SignIn(UserPasswordModel request);
    }
}