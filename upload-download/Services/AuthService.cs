using System.Text.RegularExpressions;
using upload_download.Entity;
using upload_download.Extensions;
using upload_download.Models;
using upload_download.Repository;

namespace upload_download.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepositories _userRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IUserRepositories userRepositories, IConfiguration configuration)
        {
            _userRepository = userRepositories;
            _configuration = configuration;
        }

        public async Task<bool> SignUp(EmailPasswordModel request)
        {
            try
            {
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(request.Email, emailPattern))
                {
                    throw new Exception($"{request.Email} is not a valid email address.");
                }
                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user != null)
                {
                    throw new Exception("Email already exists");
                }
                string token = Guid.NewGuid().ToString();
                string bodyEmail = EmailExtension.GenerateEmailConfirmationLink(token, request.Email);
                var reqSendEmail = new SendEmailModel { ToEmail = request.Email, Body = bodyEmail, SubJect = "Confirm your email" };
                bool status = await EmailExtension.SendEmail(AppSettingsExtension.GetSMTPSettings(_configuration), reqSendEmail);
                if (status)
                {
                    var newUser = new UserModel
                    {
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                        Email = request.Email,
                        Token = token,
                    };
                    await _userRepository.AddUser(newUser);
                }
                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<string> SignIn(EmailPasswordModel request)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    throw new Exception("Invalid email or password");
                }
                string secureKey = _configuration["Jwt:Key"];
                string issuer = _configuration["Jwt:Issuer"];
                string audience = _configuration["Jwt:Audience"];
                var token = JwtExtension.GenerateJwtToken(secureKey, issuer, audience);
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}