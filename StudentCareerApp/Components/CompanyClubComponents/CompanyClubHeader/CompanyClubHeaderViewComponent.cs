using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.CompanyClubHeaderViewComponent
{
    public class CompanyClubHeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CompClubHeaderDto model = null)
        {
            var res = model ?? new CompClubHeaderDto
            {
                EmailAddress = "info@turkcell.com.tr",
                HeaderImage = "/images/turkcell-banner.png",
                PhoneNumber = "0212 212 0000",
                ShortName = "Turkcell Platinium",
                WebSite = "https://www.turkcell.com.tr",
                SectorName = "Telekomünikasyon",
                SocialMedias = new List<SocialMediaDto>
                {
                    new SocialMediaDto{ Id = 1, SocialMediaType = SCA.Entity.Enums.SocialMediaType.Facebook, Url = "http://www.facebook.com/turkcell"},
                    new SocialMediaDto{ Id = 1, SocialMediaType = SCA.Entity.Enums.SocialMediaType.Instagram, Url = "http://www.instagram.com/turkcell"},
                    new SocialMediaDto{ Id = 1, SocialMediaType = SCA.Entity.Enums.SocialMediaType.Twitter, Url = "http://www.twitter.com/turkcell"},
                    new SocialMediaDto{ Id = 1, SocialMediaType = SCA.Entity.Enums.SocialMediaType.Youtube, Url = "http://www.youtube.com/turkcell"}
                }
            };
            return View("_CompanyClubHeader", res);
        }
    }
}
