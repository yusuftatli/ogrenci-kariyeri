using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SCA.Entity.DTO
{
    public class UserRegisterDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public GenderType GenderId { get; set; }
        public EducationType? EducationStatusId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public long? HighSchoolTypeId { get; set; }
        public string HigSchoolName { get; set; }
        public long? ClassId { get; set; }
        public int? NewGraduatedYear { get; set; }
        public long? UniversityId { get; set; }
        public long? DepartmentId { get; set; }
        public int? MasterId { get; set; }
        public int? MasterDepartment { get; set; }
        public int? MasterGraduated { get; set; }
        public bool IsStudent { get; set; }
        public string ReferanceCode { get; set; }
        public PlatformType EnrollPlatformTypeId { get; set; }
    }
}
