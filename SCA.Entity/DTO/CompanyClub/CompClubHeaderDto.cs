using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
    public class CompClubHeaderDto
    {
        public string HeaderImage { get; set; }
        public string ShortName { get; set; }
        public string WebSite { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string SectorName { get; set; }
        public List<SocialMediaDto> SocialMedias { get; set; }
    }
}
