using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Entities
{
    public class Category
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long UpdatedUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long DeletedUserId { get; set; }

        public DateTime? DeletedDate { get; set; }

        public long? ParentId { get; set; }
    }
}
