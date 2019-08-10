using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.ImageGalery
{
    public class ImageGaleryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<ImageGaleryDto> model = null)
        {
            var returnModel = model ?? FakeData();
            return View("_ImageGalery", returnModel);
        }

        private List<ImageGaleryDto> FakeData()
        {
            return new List<ImageGaleryDto>
            {
                new ImageGaleryDto
                {
                    ImagePath = "/images/kampanya-1.jpg"
                },
                new ImageGaleryDto
                {
                    ImagePath = "/images/kampanya-2.jpg"
                },
                new ImageGaleryDto
                {
                    ImagePath = "/images/kampanya-3.jpg"
                },
                new ImageGaleryDto
                {
                    ImagePath = "/images/kampanya-4.png"
                },
                new ImageGaleryDto
                {
                    ImagePath = "/images/kampanya-5.png"
                },
                new ImageGaleryDto
                {
                    ImagePath = "/images/kampanya-6.jpg"
                }
            };
        }


    }
}
