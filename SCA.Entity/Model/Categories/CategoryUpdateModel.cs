using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model.Categories
{
    public class CategoryUpdateModel
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public long? ParentId { get; set; }

        public bool IsActive { get; set; }

        public long UpdatedUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
