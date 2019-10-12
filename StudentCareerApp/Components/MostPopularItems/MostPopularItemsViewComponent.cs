using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.MostPopularItems
{
    public class MostPopularItemsViewComponent : ViewComponent
    {
        private readonly IContentManager _contentManager;
        private readonly IErrorManagement _errorManagement;

        public MostPopularItemsViewComponent(IContentManager contentManager, IErrorManagement errorManagement)
        {
            _contentManager = contentManager;
            _errorManagement = errorManagement;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count, List<ContentForHomePageDTO> model = null)
        {
            List<ContentForHomePageDTO> res = new List<ContentForHomePageDTO>();
                res = model ?? await _contentManager.GetContentForHomePage(HitTypes.MostPopuler, count);

            return View("_MostPopularItems", res);
        }
    }
}
