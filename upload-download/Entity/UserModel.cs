using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upload_download.Entity
{
    public class UserModel
    {
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? Token { get; set; }
        public bool IsActive { get; set; } = false;
    }
}