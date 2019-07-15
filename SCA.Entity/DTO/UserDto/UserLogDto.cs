using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
  public  class UserLogDto
    {
        public long UserId { get; set; }
        public DateTime EnteranceDate { get; set; }
        public string IpAddress { get; set; }
    }
}
