using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    [Table("Users", Schema = "public")]
    public class Users : BaseEntities
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string EmailAddress { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string Category { get; set; }

        [MaxLength(800)]
        public string ImagePath { get; set; }

        [ForeignKey("RoleTypeId")]
        public long RoleTypeId { get; set; }
        public RoleType RoleType { get; set; }

        public DateTime RoleExpiresDate { get; set; }
        public GenderType GenderId { get; set; }

        public EducationType EducationStatusId { get; set; }

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
        public bool IsStudent { get; set; }

        [MaxLength(500)]
        public string Biography { get; set; }

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


        public ICollection<CompanyClubs> CompanyClubs { get; set; }
        public ICollection<UserLog> UserLog { get; set; }
        public ICollection<QuesitonAsnweByUsers> QuesitonAsnweByUsers { get; set; }
        public ICollection<ReadCountOfTestAndContent> ReadCountOfTestAndContent { get; set; }
        public ICollection<SocialMedia> SocialMedia { get; set; }

    }
}
