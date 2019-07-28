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
            return View("_SocialMedia");
        }

    }
}
