using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.ImageGalery
{
    public class ImageGaleryViewComponent : ViewComponent
    {
        private readonly IImageGaleryService<SCA.Entity.Entities.ImageGalery> _imageGaleryService;

        public ImageGaleryViewComponent(IImageGaleryService<SCA.Entity.Entities.ImageGalery> imageGaleryService)
        {
            _imageGaleryService = imageGaleryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long companyClubId)
        {
            var returnModel = await _imageGaleryService.GetByWhereParams<ImageGaleryDto>(x => x.CompanyClubId == companyClubId && x.IsActive == true);
            return View("_ImageGalery", returnModel.Data as List<ImageGaleryDto>);
        }
    }
}
