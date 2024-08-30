using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upload_download.Models
{
    public class EmailPasswordModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}