using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.Anouncement
{
    public class AnouncementViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<AnouncementDto> model)
        {
            var returnModel = model ?? FakeData();
            return View("_Anouncement", returnModel);
        }

        private List<AnouncementDto> FakeData()
        {
            return new List<AnouncementDto>
            {
                new AnouncementDto
                {
                    CreatedDate = DateTime.Now.AddDays(-12),
                    EndDate = DateTime.Now.AddDays(30),
                    Description = "Black farmers in the US’s South faced with continued failure in their",
                    ImagePath = "/images/news/travel/travel5.jpg",
                    Title = "Theye back Kennedy Darlings return",
                    Url = "#"
                },
                new AnouncementDto
                {
                    CreatedDate = DateTime.Now.AddDays(-7),
                    EndDate = DateTime.Now.AddDays(7),
                    Description = "Black farmers in the US’s South faced with continued failure in their 2",
                    ImagePath = "/images/news/travel/travel5.jpg",
                    Title = "Theye back Kennedy Darlings return 2",
                    Url = "#"
                },
                new AnouncementDto
                {
                    CreatedDate = DateTime.Now.AddDays(-5),
                    EndDate = DateTime.Now.AddDays(2),
                    Description = "Black farmers in the US’s South faced with continued failure in their 3",
                    ImagePath = "/images/news/travel/travel5.jpg",
                    Title = "Theye back Kennedy Darlings return 3",
                    Url = "#"
                },
            };
        }
        
    }
}
