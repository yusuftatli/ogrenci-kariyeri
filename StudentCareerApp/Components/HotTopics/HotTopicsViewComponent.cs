using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.HotTopics
{
    public class HotTopicsViewComponent : ViewComponent
    {
        private readonly IContentManager _contentManager;

        public HotTopicsViewComponent(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count, List<ContentForHomePageDTO> model = null)
        {
            var returnModel = model ?? await _contentManager.GetContentForHomePage(HitTypes.DailyMostPopuler, count);
            return View("_HotTopics", returnModel);
        }

    }
}
