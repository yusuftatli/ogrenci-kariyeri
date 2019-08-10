using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class SocialMedia : BaseEntities
    {
        [ForeignKey("UserId")]
        public long? UserId { get; set; }
        public Users Users { get; set; }

        [ForeignKey("CompanyClupId")]
        public long? CompanyClupId { get; set; }
        public CompanyClubs CompanyClubs { get; set; }


        public string Url { get; set; }
        public bool IsActive { get; set; }
        public SocialMediaType SocialMediaType { get; set; }
    }
}
