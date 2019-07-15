using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class FacultyDto
    {
        public long Id { get; set; }
        public long FacultyCode { get; set; }
        public string FacultyName { get; set; }
        public bool IsActive { get; set; }
    }
}
