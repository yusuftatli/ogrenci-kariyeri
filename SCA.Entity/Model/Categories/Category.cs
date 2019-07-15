using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class Category : BaseEntities
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long? ParentId { get; set; }

        public ICollection<CategoryRelation> CategoryRelation { get; set; }
    }
}
