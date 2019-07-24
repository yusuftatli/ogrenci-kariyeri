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
            var fake = FakeData();
            return View("_SocialMedia", fake);
        }

        public List<SocialMediaDto> FakeData()
        {
            return new List<SocialMediaDto>
            {
                new SocialMediaDto
                {
                    FollowerCount = "5.5k",
                    Url = "http://www.facebook.com/ogrenci-kariyeri",
                    SocialMediaType = SocialMediaType.Facebook
                },
                new SocialMediaDto
                {
                    FollowerCount = "2.3k",
                    Url = "http://www.twitter.com/ogrenci-kariyeri",
                    SocialMediaType = SocialMediaType.Twitter
                },
                new SocialMediaDto
                {
                    FollowerCount = "7.4k",
                    Url = "http://www.instagram.com/ogrenci-kariyeri",
                    SocialMediaType = SocialMediaType.Instagram
                },
                new SocialMediaDto
                {
                    FollowerCount = "27.3k",
                    Url = "http://www.linkedin.com/ogrenci-kariyeri",
                    SocialMediaType = SocialMediaType.Linkedin
                },
                new SocialMediaDto
                {
                    FollowerCount = "5.1k",
                    Url = "http://www.pinterest.com/ogrenci-kariyeri",
                    SocialMediaType = SocialMediaType.Pinterest
                },
                new SocialMediaDto
                {
                    FollowerCount = "4.2k",
                    Url = "http://www.googleplus.com/ogrenci-kariyeri",
                    SocialMediaType = SocialMediaType.GooglePlus
                },
                new SocialMediaDto
                {
                    FollowerCount = "12.7k",
                    Url = "http://www.youtube.com/ogrenci-kariyeri",
                    SocialMediaType = SocialMediaType.Youtube
                },
                new SocialMediaDto
                {
                    FollowerCount = "1.2k",
                    Url = "http://www.dribble.com/ogrenci-kariyeri",
                    SocialMediaType = SocialMediaType.Dribble
                },
                new SocialMediaDto
                {
                    FollowerCount = "1.3k",
                    Url = "http://www.behance.com/ogrenci-kariyeri",
                    SocialMediaType = SocialMediaType.Behance
                }
            };
        }
    }
}
