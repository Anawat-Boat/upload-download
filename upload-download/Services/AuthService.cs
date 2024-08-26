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

        public bool SignUp(SignUpModel request)
        {
            try
            {
                var user = _userRepository.GetUserByUsername(request.UserName);
                if (user != null)
                {
                    throw new Exception("Username already exists");
                }
                var newUser = new UserModel
                {
                    UserName = request.UserName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Email = request.Email,

                };
                _userRepository.AddUser(newUser);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public string SignIn(UserPasswordModel request)
        {
            try
            {
                var user = _userRepository.GetUserByUsername(request.UserName);
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    throw new Exception("Invalid username or password");
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