using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class RolePermissonListDto
    {
        public long RolePermissonId { get; set; }
        public long MasterId { get; set; }
        public string MasterName { get; set; }
        public string MasterIcon { get; set; }
        public long DetailId { get; set; }
        public string DetailName { get; set; }
        public string DetailIcon { get; set; }
        public string DetailUrl { get; set; }
        public long RoleTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperAdmin { get; set; }

    }
}
