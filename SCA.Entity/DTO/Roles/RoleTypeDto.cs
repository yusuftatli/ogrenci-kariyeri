using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class RoleTypeDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool isActiveVal { get; set; }
        public string Menus { get; set; }
    }
}
