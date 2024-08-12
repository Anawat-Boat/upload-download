using Microsoft.AspNetCore.StaticFiles;

namespace upload_download.Extensions
{
    public class FilesExtension
    {
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
    }
}
