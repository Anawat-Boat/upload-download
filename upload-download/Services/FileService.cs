using Microsoft.AspNetCore.Mvc;
using upload_download.Extensions;
using upload_download.Models;

namespace upload_download.Services
{
    public class FileService : IFileService
    {
        private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

        public List<string> GetFiles()
        {
            try
            {
                return  Directory.GetFiles(_storagePath)
                                 .Select(Path.GetFileName)
                                 .ToList();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
       
        public async Task<UploadResponse> UploadFile(IFormFile file)
        {
            try
            {
                var filePath = Path.Combine(_storagePath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return new UploadResponse { FileName = file.FileName, PathName = filePath };
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message); 
            }
        }

    }
}
