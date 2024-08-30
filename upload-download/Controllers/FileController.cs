using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using upload_download.Extensions;
using upload_download.Models;
using upload_download.Services;
namespace upload_download.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FileController : ControllerBase
    {
        private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        [HttpGet("files")]
        public async Task<IActionResult> GetFiles()
        {
            try
            {
                List<string> files = await _fileService.GetFiles();
                return Ok(files);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No file uploaded.");

                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

                if (!FilesExtension.AllowedFileTypes.ContainsKey(fileExtension))
                    return BadRequest("File type not allowed.");

                if (!FilesExtension.AllowedFileTypes[fileExtension].Equals(file.ContentType, StringComparison.OrdinalIgnoreCase))
                    return BadRequest("File content type does not match the file extension.");

                UploadResponse result = await _fileService.UploadFile(file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filePath = Path.Combine(_storagePath, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found.");

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            string contentType = FilesExtension.GetContentType(filePath);
            memory.Position = 0;
            return File(memory, contentType, fileName);
        }
    }
}
