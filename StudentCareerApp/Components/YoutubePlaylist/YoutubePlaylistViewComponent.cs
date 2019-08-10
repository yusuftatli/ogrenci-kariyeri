using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.YoutubePlaylist
{
    public class YoutubePlaylistViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<YoutubeVideo> model = null)
        {
            var returnModel = model ?? FakeData();
            return View("_YoutubePlaylist", returnModel);
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
