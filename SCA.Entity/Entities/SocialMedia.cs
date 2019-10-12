using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Entities
{
    public class SocialMedia
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long CompanyClupId { get; set; }

        public string Url { get; set; }

        public bool IsActive { get; set; }

        public SocialMediaType SocialMediaType { get; set; }

        public long CreatedUserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public long UpdatedUserId { get; set; }

        public DateTime UpdatedDate { get; set; }

        public long DeletedUserId { get; set; }

        public DateTime DeletedDate { get; set; }
    }
}
