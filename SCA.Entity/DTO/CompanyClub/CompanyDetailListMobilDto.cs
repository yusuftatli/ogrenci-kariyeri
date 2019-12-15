using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.Entity.DTO
{
  public  class CompanyDetailListMobilDto
    {
        public  string ShortName { get; set; }
        public string SeoUrl { get; set; }
        public string SectorType { get; set; }
        public string SectorName { get; set; }
        public string HeaderImage { get; set; }
        public string ImageDirectory { get; set; }
        public string Description { get; set; }
        public string WebSite { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CompanyProfileImagePath { get; set; }
        public string Icon { get; set; }
        public List<ImageMediaListDto> ImageList { get; set; }
        public List<SocialMediaListDto> SocialMediaList{ get; set; }
    }
}
