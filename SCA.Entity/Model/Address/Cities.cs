using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SCA.Entity.Model
{
    public class Cities
    {
        [Key]
        public long CityId { get; set; }
        [StringLength(50)]
        public string CityName { get; set; }
        public int Status { get; set; }

        public ICollection<District> District { get; set; }
        public ICollection<University> University { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
