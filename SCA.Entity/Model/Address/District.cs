using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class District
    {
        [Key]
        public long DistrictId { get; set; }

        [ForeignKey("CityId")]
        public long CityId { get; set; }
        public Cities Cities { get; set; }

        [StringLength(80)]
        public string DistrictName { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
