using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class RoleType : BaseEntities
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Menus { get; set; }

        public ICollection<RolePermission> RolePermission { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
