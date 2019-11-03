using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Entity.DTO;
using SCA.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.UserPopularItems
{
    public class UserPopularItemsViewComponent : ViewComponent
    {
        private readonly IContentService<Content> _contentService;

        public UserPopularItemsViewComponent(IContentService<Content> contentService)
        {
            _contentService = contentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long userId, int count)
        {
            var @params = new
            {
                userId,
                count
            };
            var res = await _contentService.SPQueryAsync<object, ContentForHomePageDTO>(@params, "GetUserContents");
            return View("_UserPopularItems", res.Data as List<ContentForHomePageDTO>);
        }
    }
}
