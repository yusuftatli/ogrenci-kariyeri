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
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int GenderId { get; set; }
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 1 lise, 2 ünv, 3 yeni mezun
        /// </summary>
        public long EducationStatusId { get; set; }
        /// <summary>
        /// lise türü
        /// </summary>

        public long? HighSchoolTypeId { get; set; }
        public long? UniversityId { get; set; }
        /// <summary>
        /// üye sınıf 
        /// 0 hazılrık
        /// 1,2,3,4,5,6
        /// </summary>
        public long? ClassId { get; set; }
        /// <summary>
        /// bölüm
        /// </summary>
        public long? DepartmentId { get; set; }

        public long NewGraduatedYear { get; set; }

        public long? CityId { get; set; }

        public string ReferanceCode { get; set; }

        public string Biography { get; set; }
        public string push { get; set; }
        public string UserRegisterPlatformId { get; set; }


        public string HigSchoolName { get; set; }
        public int MasterId { get; set; }
        public int MasterDepartment { get; set; }
        public int MasterGraduated { get; set; }

    }
}
