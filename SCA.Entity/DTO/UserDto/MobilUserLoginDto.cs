using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
   public class MobilUserLoginDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string push { get; set; }
        public string UserRegisterPlatformId { get; set; }
    }
}
