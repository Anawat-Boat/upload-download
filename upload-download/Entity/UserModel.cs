using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upload_download.Entity
{
    public class UserModel
    {
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; } // Store hashed passwords
    }
}