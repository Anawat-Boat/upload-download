using Microsoft.AspNetCore.Mvc;
using upload_download.Models;
using upload_download.Services;

namespace upload_download.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("sign-up")]
        public IActionResult SignUp([FromBody] SignUpModel request)
        {
            try
            {
                bool result = _authService.SignUp(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("sign-in")]
        public IActionResult SignIn([FromBody] UserPasswordModel request)
        {
            try
            {
                string token = _authService.SignIn(request);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}