using SCA.Entity.Model;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class University : BaseEntities
    {
        public long UniversityCode { get; set; }

        [StringLength(200)]
        public string UniversityName { get; set; }

        [ForeignKey("CityId")]
        public long CityId { get; set; }
        public Cities Cities { get; set; }

        public bool IsActive { get; set; }

    }
}
