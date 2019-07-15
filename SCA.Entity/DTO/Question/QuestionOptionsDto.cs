using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class QuestionOptionsDto
    {
        public long Id { get; set; }
        public long QuestionId { get; set; }
        public bool CheckOption { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string FreeText { get; set; }
        public int Answer { get; set; }
    }
}
