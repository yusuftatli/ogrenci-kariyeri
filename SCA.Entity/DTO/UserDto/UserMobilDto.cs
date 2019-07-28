using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class UserMobilDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Category { get; set; }
        public string ImagePath { get; set; }
        public long RoleTypeId { get; set; }
        public long EducationStatusId { get; set; }
        public EducationStatus EducationStatus { get; set; }

        public DateTime BirthDate { get; set; }

        public long? HighSchoolTypeId { get; set; }

        public long? UniversityId { get; set; }

        public long? FacultyId { get; set; }

        public long? DepartmentId { get; set; }

        public long? ClassId { get; set; }

        public long? CityId { get; set; }

        public bool IsActive { get; set; }
        public string ReferanceCode { get; set; }
    }
}
