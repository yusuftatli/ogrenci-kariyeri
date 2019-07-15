using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SCA.Entity.Model
{
    public class Tags : BaseEntities
    {
        [StringLength(100)]
        public string Description { get; set; }
        public long Hit { get; set; }

        public ICollection<TagRelation> TagRelation { get; set; }
    }
}
