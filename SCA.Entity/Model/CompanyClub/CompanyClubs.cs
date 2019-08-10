using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.Model
{
    public class CompanyClubs : BaseEntities
    {
        public CompanyClupType CompanyClupType { get; set; }
        public string ShortName { get; set; }
        public string Sector { get; set; }



        public ICollection<SocialMedia> SocialMedia { get; set; }
    }
}
