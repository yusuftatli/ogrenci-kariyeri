using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model.Categories
{
    public class CategoryInsertModel
    {
        public string Description { get; set; }

        public long? ParentId { get; set; }

        public bool IsActive { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
