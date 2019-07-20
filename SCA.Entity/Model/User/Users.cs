using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class Users : BaseEntities
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Category { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey("RoleTypeId")]
        public long RoleTypeId { get; set; }
        public RoleType RoleType { get; set; }

        public DateTime RoleExpiresDate { get; set; }
        public GenderType GenderId { get; set; }

        [ForeignKey("EducationStatusId")]
        public long EducationStatusId { get; set; }
        public EducationStatus EducationStatus { get; set; }

        public DateTime BirthDate { get; set; }

        [ForeignKey("HighSchoolTypeId")]
        public long? HighSchoolTypeId { get; set; }
        public HighSchool HighSchoolType { get; set; }

        [ForeignKey("UniversityId")]
        public long? UniversityId { get; set; }
        public University University { get; set; }

        [ForeignKey("FacultyId")]
        public long? FacultyId { get; set; }
        public Faculty Faculty { get; set; }

        [ForeignKey("DepartmentId")]
        public long? DepartmentId { get; set; }
        public Department Department { get; set; }

        [ForeignKey("ClassId")]
        public long? ClassId { get; set; }
        public StudentClass ClassType { get; set; }

        [ForeignKey("CityId")]
        public long? CityId { get; set; }
        public Cities Cities { get; set; }

        //[ForeignKey("DistrictId")]
        //public long DistrictId { get; set; }
        //public District District { get; set; }

        //public long DefaultPageId { get; set; }

        public bool IsActive { get; set; }
        public string ReferanceCode { get; set; }
        public PlatformType EnrollPlatformTypeId { get; set; }


        public ICollection<Comments> Comments { get; set; }
        public ICollection<UserLog> UserLog { get; set; }
        public ICollection<QuesitonAsnweByUsers> QuesitonAsnweByUsers { get; set; }
        public ICollection<ReadCountOfTestAndContent> ReadCountOfTestAndContent { get; set; }
        public ICollection<MenuRelationWithRole> MenuRelationWithUser { get; set; }

    }
}
