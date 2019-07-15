using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class CategoryRelation : BaseEntities
    {
        [ForeignKey("CategoryId")]
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public long TagContentId { get; set; }
        public ReadType ReadType { get; set; }
    }
}
