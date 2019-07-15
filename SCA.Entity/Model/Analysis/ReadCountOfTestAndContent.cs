using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class ReadCountOfTestAndContent : BaseEntities
    {
        [ForeignKey("TestId")]
        public long TestId { get; set; }
        public Tests Tests { get; set; }

        [ForeignKey("UserId")]
        public long UserId { get; set; }
        public Users Users { get; set; }

        public ReadType ReadType { get; set; }
        public long ReadId { get; set; }
        public PlatformType PlatformType { get; set; }
        public long Count { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
