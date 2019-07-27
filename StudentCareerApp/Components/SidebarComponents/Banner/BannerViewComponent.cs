using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.SidebarComponents.Banner
{
    public class BannerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string url, string imagePath)
        {
            ViewBag.Url = url;
            ViewBag.ImagePath = imagePath;
            return View("_Banner");
        }
    }
}
