using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.CategoryNews
{
    public class CategoryNewsViewComponent : ViewComponent
    {
        private readonly IContentManager _contentManager;

        public CategoryNewsViewComponent(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count, List<ContentForHomePageDTO> model = null)
        {
            var returnModel = model ?? await _contentManager.GetContentForHomePage(SCA.Entity.Enums.HitTypes.CategoryNews, count);
            return View("deneme");
        }
    } 
}
