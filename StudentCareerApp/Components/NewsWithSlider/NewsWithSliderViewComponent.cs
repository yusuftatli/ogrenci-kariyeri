using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.NewsWithSlider
{
    public class NewsWithSliderViewComponent : ViewComponent
    {
        private readonly IContentManager _contentManager;

        public NewsWithSliderViewComponent(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public async Task<IViewComponentResult> Invoke(int count, List<ContentForHomePageDTO> model = null)
        {
            var res = model ?? await _contentManager.GetContentForHomePage(HitTypes.HeadLine, count);
            return View("_NewsWithSlider", model);
        }
    }
}
