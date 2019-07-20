using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class LoginDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool RememberMe { get; set; }
    }
}
