using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
  public  class UserLogDto
    {
        public long UserId { get; set; }
        public PlatformType PlatformTypeId { get; set; }
        public DateTime EnteraceDate { get; set; }
        public string IpAddress { get; set; }
    }
}
