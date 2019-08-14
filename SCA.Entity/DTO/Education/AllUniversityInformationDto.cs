using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class AllUniversityInformationDto
    {
        public List<EducationStatusDto> EducationTypes { get; set; }
        public List<UniversityDto> Universities { get; set; }
        public List<FacultyDto> Faculties { get; set; }
        public List<DepartmentDto> Departments { get; set; }
        public List<StudentClassDto> Classes { get; set; }
        public List<HighSchoolDto> HighSchools { get; set; }
        public List<CitiesDto> Cities { get; set; }
        public UserRegisterDto RegisterDto { get; set; }
    }
}
