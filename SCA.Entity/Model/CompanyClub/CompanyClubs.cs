using SCA.Entity.Enums;
using SCA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SCA.Entity.Model
{
    public class CompanyClubs : BaseEntities
    {
        public CompanyClupType CompanyClupType { get; set; }
        public string ShortName { get; set; }
        public SectorType SectorType { get; set; }

        public string SeoUrl { get; set; }
        public string HeaderImage { get; set; }

        [ForeignKey("SectorId")]
        public long SectorId { get; set; }
        public Sector Sector { get; set; }


        [ForeignKey("UserId")]
        public long? UserId { get; set; }
        public Users Users { get; set; }

        public string CreateUserName { get; set; }

        public string Description { get; set; }
        public string WebSite { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }


        public ICollection<SocialMedia> SocialMedia { get; set; }
        public ICollection<ImageGalery> ImageGalery { get; set; }

    }
}
