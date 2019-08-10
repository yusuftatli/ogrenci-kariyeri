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

        public IViewComponentResult Invoke(int count = 10, List<ContentForHomePageDTO> model = null)
        {
            var res = model ?? _contentManager.GetContentForHomePage(HitTypes.MostPopuler, count).Result;
            return View("_MostPopularItems", model);
        }
    }
}
