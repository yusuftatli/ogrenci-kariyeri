using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class SocialMedia : BaseEntities
    {
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public SocialMediaType SocialMediaType { get; set; }
    }
}
