using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class QuestionOptions : BaseEntities
    {
        [ForeignKey("QuestionId")]
        public long QuestionId { get; set; }
        public Questions Questions { get; set; }

        public bool CheckOption { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string FreeText { get; set; }
        public int Answer { get; set; }
    }
}
