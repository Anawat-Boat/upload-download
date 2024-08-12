using Microsoft.AspNetCore.StaticFiles;

namespace upload_download.Extensions
{
    public class FilesExtension
    {
        // Allowed file types
        public static readonly Dictionary<string, string> AllowedFileTypes = new Dictionary<string, string>
        {
            { ".jpg", "image/jpeg" },
            { ".png", "image/png" },
            { ".txt", "text/plain" },
            { ".pdf", "application/pdf" }
        };

        public static string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }


        public static byte[] GetFileContent(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("The requested file was not found.", filePath);
            }

            return File.ReadAllBytes(filePath);
        }
    }
}
