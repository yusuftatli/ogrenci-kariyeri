using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class RolePermission : BaseEntities
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("RoleTypeId")]
        public long RoleTypeId { get; set; }
        public RoleType RoleType { get; set; }
    }
}
