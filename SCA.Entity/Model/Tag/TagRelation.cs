using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class TagRelation : BaseEntities
    {
        [ForeignKey("TagId")]
        public long TagId { get; set; }
        public Tags Tags { get; set; }
        public long TagContentId { get; set; }
        public ReadType ReadType { get; set; }
    }
}
