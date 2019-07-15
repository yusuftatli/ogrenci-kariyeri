using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class TestValue : BaseEntities
    {
        [ForeignKey("TestId")]
        public long TestId { get; set; }
        public Tests Tests { get; set; }

        public int FirstValue { get; set; }
        public int SecondValue { get; set; }
        public string Description { get; set; }
    }
}
