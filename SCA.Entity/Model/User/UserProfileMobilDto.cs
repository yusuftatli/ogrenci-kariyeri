using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class UserProfileMobilDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public long GenderId { get; set; }
        public long EducationStatusId { get; set; }
        public long HighSchoolTypeId { get; set; }
        public long UniversityId { get; set; }
        public string push { get; set; }
        public long DepartmentId { get; set; }
        public long ClassId { get; set; }
        public string Biography { get; set; }
        public string CityId { get; set; }
        public string ReferanceCode { get; set; }
        public DateTime BirthDate { get; set; }
        public long NewGraduatedYear { get; set; }
        public string HigSchoolName { get; set; }
        public int MasterId { get; set; }
        public int MasterDepartment { get; set; }
        public int MasterGraduated { get; set; }
    }
}
