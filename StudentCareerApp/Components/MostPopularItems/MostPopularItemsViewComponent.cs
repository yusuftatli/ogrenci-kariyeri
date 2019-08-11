using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.MostPopularItems
{
    public class MostPopularItemsViewComponent : ViewComponent
    {
        private readonly IContentManager _contentManager;

        public MostPopularItemsViewComponent(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count, List<ContentForHomePageDTO> model = null)
        {
            var res = model ?? await _contentManager.GetContentForHomePage(HitTypes.MostPopuler, count);
            return View("_MostPopularItems", res);
        }
    }
}
