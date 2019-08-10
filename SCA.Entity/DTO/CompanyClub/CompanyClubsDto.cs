using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
   public class CompanyClubsDto
    {
        public long Id { get; set; }
        public CompanyClupType CompanyClupType { get; set; }
        public string ShortName { get; set; }
        public SectorType SectorType { get; set; }

        public string HeaderImage { get; set; }

        public long SectorId { get; set; }
        public long? UserId { get; set; }

        public string Description { get; set; }
        public string WebSite { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
