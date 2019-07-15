using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class MenuRelationWithRole : BaseEntities
    {
        [ForeignKey("MenuId")]
        public long MenuId { get; set; }
        public MenuList MenuList { get; set; }

        [ForeignKey("RoleId")]
        public long RoleId { get; set; }
        public RoleType RoleType { get; set; }
    }
}
