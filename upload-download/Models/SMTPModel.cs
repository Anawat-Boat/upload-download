using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upload_download.Models
{
    public class SMTPModel
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}