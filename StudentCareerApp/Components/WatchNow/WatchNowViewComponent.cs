using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.WatchNow
{
    public class WatchNowViewComponent : ViewComponent
    {
        private readonly IContentManager _contentManager;

        public WatchNowViewComponent(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public async Task<IViewComponentResult> Invoke(int count, List<ContentForHomePageDTO> model = null)
        {
            var res = model ?? await _contentManager.GetContentForHomePage(HitTypes.ConstantMainMenu, count);
            return View("_WatchNow", model);
        }
    }
}
