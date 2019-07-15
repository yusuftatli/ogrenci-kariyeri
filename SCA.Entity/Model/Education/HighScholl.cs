using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class HighSchool : BaseEntities
    {
        public long HighSchoolCode { get; set; }
        public string SchoolName { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("CityId")]
        public long CityId { get; set; }
        public Cities Cities { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
