using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class StudentClass : BaseEntities
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
