using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class UserForgatPasswordDto
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
