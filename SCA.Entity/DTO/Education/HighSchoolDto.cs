using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class HighSchoolDto
    {
        public long Id { get; set; }
        public long HighSchoolCode { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }
        public string SchoolName { get; set; }
        public bool IsActive { get; set; }
    }
}
