using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class DefaultDatalarMobilDto
    {
        public int MinimumVersion { get; set; } = 300;
        public string Adverds { get; set; }
        public List<HighSchoolMobilDto> HighSchoolList { get; set; }
        public List<UniverstiyMobilDto> UniversityList { get; set; }
        public List<CategoriesMobilDto> CategoriesList { get; set; }
        public List<DepartmentMobilDto> DepartmentList { get; set; }
        public List<CitiesMobilDto> CityList { get; set; }
    }
}
