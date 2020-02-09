using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Entities
{
    public class Users
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Category { get; set; }

        public string ImagePath { get; set; }

        public long RoleTypeId { get; set; }

        public DateTime RoleExpiresDate { get; set; }

        public GenderType GenderId { get; set; }

        public EducationType EducationStatusId { get; set; }

        public long? HighSchoolTypeId { get; set; }

        public long? UniversityId { get; set; }

        public long? FacultyId { get; set; }

        public string push { get; set; }

        public long? DepartmentId { get; set; }

        public long? ClassId { get; set; }

        public bool IsActive { get; set; }

        public string ReferanceCode { get; set; }

        public PlatformType EnrollPlatformTypeId { get; set; }

        public DateTime LastEntrance { get; set; }

        public DateTime BirthDate { get; set; }

        public string Biography { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedUserId { get; set; }

        public DateTime UpdatedDate { get; set; }

        public long? DeletedUserId { get; set; }

        public DateTime DeletedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string ForgetAuthorizationKey { get; set; }

        public DateTime ForgetAuthorizationExpiryDate { get; set; }

    }
}
