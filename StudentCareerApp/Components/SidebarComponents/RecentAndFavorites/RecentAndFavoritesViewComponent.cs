using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.SidebarComponents.RecentAndFavorites
{
    public class RecentAndFavoritesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = FakeData();
            return View("_RecentAndFavorites", model);
        }

        public RecentAndFavoritesContentForUIDto FakeData()
        {
            return new RecentAndFavoritesContentForUIDto
            {
                Favorites = new List<ContentForHomePageDTO>
                {
                    new ContentForHomePageDTO
                    {
                        Category = "Teknoloji",
                        Header = "Elon Musk yeni teknoloji şirketini duyurdu. Intellicore!",
                        ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                        IsHeadLine = true,
                        IsManset = true,
                        PublishDate = new DateTime(2019, 7, 11),
                        ReadCount = 122,
                        SeoUrl = "elon-musk-yeni-teknoloji-sirketini-duyurdu-intellicore",
                        Writer = "Göksu Deniz"
                    },
                    new ContentForHomePageDTO
                    {
                        Category = "Teknoloji",
                        Header = "SpaceX, Çin ile yarışıyor! Uzaya gönderilen roket sayısında inanılmaz rekabet!",
                        ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                        IsHeadLine = true,
                        IsManset = true,
                        PublishDate = new DateTime(2019, 7, 11),
                        ReadCount = 122,
                        SeoUrl = "spacex-cin-ile-yarisiyor-uzaya-gonderilen-roket-sayisinda-inanilmaz-rekabet",
                        Writer = "Göksu Deniz"
                    },
                    new ContentForHomePageDTO
                    {
                        Category = "Eğitim",
                        Header = "UIPath, RPA alanında Türkiye'de ücretsiz serfikasyon programına başladı.",
                        ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                        IsHeadLine = true,
                        IsManset = true,
                        PublishDate = new DateTime(2019, 7, 11),
                        ReadCount = 74,
                        SeoUrl = "uipath-rpa-alaninda-turkiyede-ucretsiz-sertifikasyon-programina-basladi",
                        Writer = "Göksu Deniz"
                    },
                    new ContentForHomePageDTO
                    {
                        Category = "Sağlık",
                        Header = "Kansere yakın zamanda tedavi bulunabilir. İsrailli bilim adamları oral yolla alınan ilaç geliştirdi.",
                        ImagePath = "/AdminFiles/CMS/Content/Images/3.jpg",
                        IsHeadLine = true,
                        IsManset = true,
                        PublishDate = new DateTime(2019, 7, 11),
                        ReadCount = 122,
                        SeoUrl = "kansere-yakin-zamanda-tedavi-bulunabilir-israilli-bilim-adamlari-oral-yolla-alinan-ilac-gelistirdi",
                        Writer = "Göksu Deniz"
                    }
                },
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
