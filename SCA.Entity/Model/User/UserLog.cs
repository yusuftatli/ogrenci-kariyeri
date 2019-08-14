using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    [Table("UserLog", Schema = "public")]
    public class UserLog : BaseEntities
    {
        [ForeignKey("UserId")]
        public long UserId { get; set; }
        public Users Users { get; set; }

        public PlatformType PlatformTypeId { get; set; }

        public DateTime EnteraceDate { get; set; }
        public string IpAddress { get; set; }
    }
}
