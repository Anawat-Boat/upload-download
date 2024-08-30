using upload_download.Extensions;
using upload_download.Models;
using upload_download.Repository;

namespace upload_download.Services
{
    public class EmailService : IEmailService
    {
        private readonly IUserRepositories _userRepository;
        public EmailService(IUserRepositories userRepositories)
        {
            _userRepository = userRepositories;
        }
        public async Task<bool> ConfirmEmail(string token, string email)
        {
            try
            {
                var user = _userRepository.GetUserByTokenAndEmail(token, email);
                if (user == null)
                {
                    return false;
                }
                await _userRepository.ActiveUser(email);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}