using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
   public class RolePermissionDto
    {
        public long Id { get; set; }
        public long SreenMasterId { get; set; }
        public long ScreenDetailId { get; set; }
        public bool IsActive { get; set; }
        public long RoleTypeId { get; set; }
    }
}
