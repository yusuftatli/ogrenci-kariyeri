using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.WatchNow
{
    public class WatchNowViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(List<ContentForHomePageDTO> model)
        {
            return View("_WatchNow", model);
        }
    }
}
