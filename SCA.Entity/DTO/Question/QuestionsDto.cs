using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class QuestionsDto
    {
        public long Id { get; set; }
        public long TestId { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

       public List<QuestionOptionsDto> QuestionOptionList { get; set; }
    }
}
