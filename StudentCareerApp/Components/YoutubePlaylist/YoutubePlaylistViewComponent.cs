using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.YoutubePlaylist
{
    public class YoutubePlaylistViewComponent : ViewComponent
    {
        private readonly ICompanyClubManager _companyManager;

        public YoutubePlaylistViewComponent(ICompanyClubManager companyManager)
        {
            _companyManager = companyManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string seoUrl, List<YoutubeVideo> model = null)
        {
            var returnModel = await _companyManager.GetCompanyYoutubePlayList(seoUrl);
            return View("_YoutubePlaylist", returnModel.Data as List<YoutubeVideo>);
        }

        public List<YoutubeVideo> FakeData()
        {
            return new List<YoutubeVideo>
            {
                new YoutubeVideo
                {
                    ImagePath = "/images/news/sports/sports4.jpg",
                    Title = "Tourism in Dubai tourist favorite place",
                    VideoLink = "https://www.youtube.com/watch?v=_0UO1NcheAE"
                },
                new YoutubeVideo
                {
                    ImagePath = "/images/news/sports/sports4.jpg",
                    Title = "Tourism in Dubai tourist favorite place",
                    VideoLink = "https://www.youtube.com/watch?v=_0UO1NcheAE"
                },
                new YoutubeVideo
                {
                    ImagePath = "/images/news/sports/sports4.jpg",
                    Title = "Tourism in Dubai tourist favorite place",
                    VideoLink = "https://www.youtube.com/watch?v=_0UO1NcheAE"
                },
                new YoutubeVideo
                {
                    ImagePath = "/images/news/sports/sports4.jpg",
                    Title = "Tourism in Dubai tourist favorite place",
                    VideoLink = "https://www.youtube.com/watch?v=_0UO1NcheAE"
                },
                new YoutubeVideo
                {
                    ImagePath = "/images/news/sports/sports4.jpg",
                    Title = "Tourism in Dubai tourist favorite place",
                    VideoLink = "https://www.youtube.com/watch?v=_0UO1NcheAE"
                }
            };
        }
    }
}
