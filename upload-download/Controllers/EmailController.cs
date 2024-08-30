using System.Web;
using Microsoft.AspNetCore.Mvc;
using upload_download.Models;
using upload_download.Services;

namespace upload_download.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token, [FromQuery] string email)
        {
            try
            {
                // Decode the token and email if necessary
                var decodedToken = HttpUtility.UrlDecode(token);
                var decodedEmail = HttpUtility.UrlDecode(email);
                bool result = await _emailService.ConfirmEmail(decodedToken, decodedEmail);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}