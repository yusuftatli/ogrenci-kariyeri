using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.Slider
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly IContentManager _contentManager;

        public SliderViewComponent(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var res = new SliderContentDto();
            res.SliderContents = await _contentManager.GetContentForHomePage(SCA.Entity.Enums.HitTypes.HeadLine, count);
            
            return View("_Slider", res);
        }
    }
}
