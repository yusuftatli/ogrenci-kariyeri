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
            var model = FakeData();
            if(HttpContext.GetSessionData<UserSession>("userInfo")?.Id > 0)
            {
                model.Favorites = await _contentManager.GetUsersFavoriteContents(HttpContext.GetSessionData<UserSession>("userInfo").Id, count);
            }
            return View("_RecentAndFavorites", model);
        }

        public RecentAndFavoritesContentForUIDto FakeData()
        {
            return new RecentAndFavoritesContentForUIDto
            {
                Favorites = null,
                Recents = new List<ContentForHomePageDTO>
                {
                    new ContentForHomePageDTO
                    {
                        Category = "Sağlık",
                        Header = "Bu ülkelere gitme planınız varsa tekrar düşünmenizi gerektirecek bazı durumlar",
                        ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                        IsHeadLine = true,
                        IsManset = true,
                        PublishDate = new DateTime(2019, 7, 11),
                        ReadCount = 196,
                        SeoUrl = "bu-ulkelere-gitme-planiniz-varsa-tekrar-dusunmenizi-gerektirecek-bazi-durumlar",
                        Writer = "Göksu Deniz"
                    },
                    new ContentForHomePageDTO
                    {
                        Category = "Biyoloji",
                        Header = "İstanbul Üniversitesi'nin gece yeşil ışık saçan tavşanları",
                        ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                        IsHeadLine = true,
                        IsManset = true,
                        PublishDate = new DateTime(2019, 7, 11),
                        ReadCount = 45,
                        SeoUrl = "istanbul-universitesinin-gece-yesil-isik-sacan-tavsanlari",
                        Writer = "Göksu Deniz"
                    },
                    new ContentForHomePageDTO
                    {
                        Category = "Biyoloji",
                        Header = "Farelerde bulunan gen tek eşliliğin sırrı olabilir. İşte o genin ne işe yaradığı!",
                        ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                        IsHeadLine = true,
                        IsManset = true,
                        PublishDate = new DateTime(2019, 7, 11),
                        ReadCount = 12,
                        SeoUrl = "farelerde-bulunan-gen-tek-esliligin-sirri-olabilir-iste-o-genin-ne-ise-yaradigi",
                        Writer = "Göksu Deniz"
                    }
                }
            };
        }


    }
}
