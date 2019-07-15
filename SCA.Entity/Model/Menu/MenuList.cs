using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class MenuList : BaseEntities
    {
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public string Icon { get; set; }
        public long? ParentId { get; set; }

        public ICollection<MenuRelationWithRole> MenuRelationWithUser { get; set; }
    }
}
