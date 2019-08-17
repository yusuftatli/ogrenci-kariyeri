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
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public GenderType GenderId { get; set; }
        public EducationType EducationStatusId { get; set; }
        public DateTime BirthDate { get; set; }
        public long? HighSchoolTypeId { get; set; }
        public long? UniversityId { get; set; }
        public long? FacultyId { get; set; }
        public long? DepartmentId { get; set; }
        public long? ClassId { get; set; }
        public bool IsStudent { get; set; }
        public string Password { get; set; }
        public string RetypePassword { get; set; }
        public string ReferanceCode { get; set; }
        public bool IsEmailSend { get; set; }
        public bool IsPhoneSend { get; set; }
        public bool IsActive { get; set; }
        public bool SubscribeNewsletter { get; set; }
        public string ImageData { get; set; }
        public int BanCount { get; set; }
        public PlatformType EnrollPlatformTypeId { get; set; }
    }
}
