using Microsoft.AspNetCore.Mvc;
using SCA.Common;
using SCA.Entity.DTO;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.SidebarComponents.RecentAndFavorites
{
    public class RecentAndFavoritesViewComponent : ViewComponent
    {
        private readonly IContentManager _contentManager;

        public RecentAndFavoritesViewComponent(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int count) 
        {
            var model = new RecentAndFavoritesContentForUIDto();
            if(HttpContext.GetSessionData<UserSession>("userInfo")?.Id > 0)
            {
                model.Favorites = await _contentManager.GetUsersFavoriteContents(HttpContext.GetSessionData<UserSession>("userInfo").Id, count);
                model.Recents = await _contentManager.GetContentForHomePage(SCA.Entity.Enums.HitTypes.LastAssay, 5);
            }
            return View("_RecentAndFavorites", model);
        }
    }
}
