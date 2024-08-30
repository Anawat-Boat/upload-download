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
        public async Task<IActionResult> SignUp([FromBody] EmailPasswordModel request)
        {
            try
            {

                bool result = await _authService.SignUp(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] EmailPasswordModel request)
        {
            try
            {
                string token = await _authService.SignIn(request);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}