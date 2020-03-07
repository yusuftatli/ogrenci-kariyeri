using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class ActivityMotionDto
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ScoolName { get; set; }
        public string Department { get; set; }
        public string Class { get; set; }
        public string Message { get; set; }
        public int UniversityId { get; set; }
        public int ActivityId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
