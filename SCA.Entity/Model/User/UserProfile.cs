using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class UserProfile
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string ImagePath { get; set; }

        public GenderType GenderId { get; set; }

        public EducationType EducationStatusId { get; set; }

        public long? HighSchoolTypeId { get; set; }

        public long? UniversityId { get; set; }

        public long? FacultyId { get; set; }

        public long? DepartmentId { get; set; }

        public long? ClassId { get; set; }

        public DateTime BirthDate { get; set; }

        public string Biography { get; set; }

    }
}
