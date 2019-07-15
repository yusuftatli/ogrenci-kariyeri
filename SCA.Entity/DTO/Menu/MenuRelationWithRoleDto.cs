using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class MenuRelationWithRoleDto
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public long RoleId { get; set; }
    }
}
