using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.SidebarComponents.PopularCategories
{
    public class PopularCategoriesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var fakeData = FakeData();
            return View("_PopularCategories", fakeData);
        }

        private List<PopularCategoriesDto> FakeData()
        {
            return new List<PopularCategoriesDto>
            {
                new PopularCategoriesDto
                {
                    Description = "Teknoloji",
                    SeoUrl = "teknoloji",
                    UsingCount = 26
                },
                new PopularCategoriesDto
                {
                    Description = "Sağlık",
                    SeoUrl = "saglik",
                    UsingCount = 7
                },
                new PopularCategoriesDto
                {
                    Description = "Spor",
                    SeoUrl = "spor",
                    UsingCount = 21
                },
                new PopularCategoriesDto
                {
                    Description = "Tarih",
                    SeoUrl = "tarih",
                    UsingCount = 20
                },
                new PopularCategoriesDto
                {
                    Description = "Bilim",
                    SeoUrl = "bilim",
                    UsingCount = 34
                },
                new PopularCategoriesDto
                {
                    Description = "Eğitim",
                    SeoUrl = "egitim",
                    UsingCount = 43
                },
                new PopularCategoriesDto
                {
                    Description = "İş",
                    SeoUrl = "is",
                    UsingCount = 10
                },
                new PopularCategoriesDto
                {
                    Description = "Seyahat",
                    SeoUrl = "seyahat",
                    UsingCount = 6
                }
            };
        }
    }
}
