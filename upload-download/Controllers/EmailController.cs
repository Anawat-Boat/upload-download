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
        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailModel request)
        {
            try
            {
                bool result = await _emailService.SendEmail(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}