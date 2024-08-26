using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upload_download.Models
{
    public class SendEmailModel
    {
        public required string ToEmail { get; set; }
        public string? SubJect { get; set; }
        public string? Body { get; set; }
    }
}