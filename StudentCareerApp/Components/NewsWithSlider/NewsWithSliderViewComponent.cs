using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.NewsWithSlider
{
    public class NewsWithSliderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<ContentForHomePageDTO> model)
        {
            return View("_NewsWithSlider", model);
        }
    }
}
