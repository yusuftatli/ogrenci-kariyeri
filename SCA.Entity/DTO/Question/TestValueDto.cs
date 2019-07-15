using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class TestValueDto
    {
        public long Id { get; set; }
        public long TestId { get; set; }
        public int FirstValue { get; set; }
        public int SecondValue { get; set; }
        public string Description { get; set; }
    }
}
