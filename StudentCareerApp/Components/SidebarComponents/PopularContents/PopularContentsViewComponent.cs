using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.SidebarComponents.PopularContents
{
    public class PopularContentsViewComponent : ViewComponent
    {
        private readonly IContentManager _contentManager;

        public PopularContentsViewComponent(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count, List<ContentForHomePageDTO> model = null)
        {
            if (model == null)
            {
                var res = model ?? await _contentManager.GetContentForHomePage(SCA.Entity.Enums.HitTypes.MostPopuler, count);
                return View("_PopularContents", res);
            }
            return View("_PopularContents", model);
        }
    }
}
