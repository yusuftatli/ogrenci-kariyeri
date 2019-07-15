using SCA.Entity.Model;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class Department : BaseEntities
    {
        public long DepartmentCode { get; set; }
        public bool IsActive { get; set; }

        [StringLength(200)]
        public string DepartmentName { get; set; }

        public int Status { get; set; }
    }
}
