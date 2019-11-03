using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Entities
{
    public class BasicPages
    {
        public long Id { get; set; }

        public string ImagePath { get; set; }

        public string SeoUrl { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? UpdatedUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? DeletedUserId { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
