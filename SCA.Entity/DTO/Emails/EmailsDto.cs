using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class EmailsDto
    {
        public long UserId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
        public bool IsSend { get; set; }
        public DateTime SendDate { get; set; }
        public string Process { get; set; }
    }
}
