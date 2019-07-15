using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class DepartmentDto
    {
        public long Id { get; set; }
        public long DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
    }
}
