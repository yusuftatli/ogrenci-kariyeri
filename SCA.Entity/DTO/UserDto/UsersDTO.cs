using SCA.Entity.Enums;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class UsersDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Biography { get; set; }
        public string Category { get; set; }
        public long RoleTypeId { get; set; }
        public string RoleType { get; set; }
        public int BanCount { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
        public string ImageData { get; set; }
        public DateTime RoleExpiresDate { get; set; }
        public string Gender { get; set; }
        public EducationType? EducationStatusId { get; set; }
        public DateTime BirthDate { get; set; }
        public long? HighSchoolTypeId { get; set; }
        public long? UniversityId { get; set; }
        public long? FacultyId { get; set; }
        public long? DepartmentId { get; set; }
        public long ClassId { get; set; }
        public long? CityId { get; set; }
        public long DistrictId { get; set; }
        public string ReferanceCode { get; set; }
        public long DefaultPageId { get; set; }
        public PlatformType EnrollPlatformTypeId { get; set; }
        public bool IsEmailSend { get; set; }
        public bool IsPhoneSend { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string Instagram { get; set; }
    }
}
