using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upload_download.Models
{
    public class SignUpModel : UserPasswordModel
    {
        public string Email { get; set; }
    }
}