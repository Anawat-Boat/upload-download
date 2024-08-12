using Microsoft.AspNetCore.Mvc;
using upload_download.Models;

namespace upload_download.Services
{
    public interface IFileService
    {
        List<string> GetFiles();
        Task<UploadResponse> UploadFile(IFormFile file);
    }
}
