using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class Questions : BaseEntities
    {
        [ForeignKey("TestId")]
        public long TestId { get; set; }
        public Tests Tests { get; set; }

        public string ImagePath { get; set; }
        public string Description { get; set; }

        public ICollection<QuestionOptions> QuestionOptions { get; set; }
    }
}
