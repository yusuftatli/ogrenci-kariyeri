using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.SidebarComponents.SocialMedia
{
    public class SocialMediaViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = FakeData();
            return View("_SocialMedia", model);
        }

        private List<SocialMediaDto> FakeData()
        {
            return new List<SocialMediaDto>
            {
                new SocialMediaDto
                {
                    Id = 1,
                    SocialMediaType = SocialMediaType.Facebook,
                    Url = "https://www.facebook.com"
                },
                new SocialMediaDto
                {
                    Id = 2,
                    SocialMediaType = SocialMediaType.Instagram,
                    Url = "https://www.instagram.com"
                },
                new SocialMediaDto
                {
                    Id = 3,
                    SocialMediaType = SocialMediaType.Twitter,
                    Url = "https://www.twitter.com"
                }
            };
        }

    }
}
