using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class UniversityDto
    {
        public long Id { get; set; }
        public long UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public long CityId { get; set; }
        public string CityName { get; set; }
        public bool IsActive { get; set; }
    }
}
