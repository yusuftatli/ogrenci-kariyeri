using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.Menu
{
    public class HeaderMenuViewComponent : ViewComponent
    {
        private readonly IBasicPagesService<SCA.Entity.Entities.BasicPages> _basicPageService;

        public HeaderMenuViewComponent(IBasicPagesService<SCA.Entity.Entities.BasicPages> basicPageService) => _basicPageService = basicPageService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = await _basicPageService.GetByWhereParams<SCA.Entity.Model.BasicPages.BasicPageForMenu>(x => x.IsActive == true && (x.TypeOfPage == SCA.Entity.Enums.PageType.HeaderPage || x.TypeOfPage == SCA.Entity.Enums.PageType.BothOfThem));
            return View("_HeaderMenu", res.Data as List<SCA.Entity.Model.BasicPages.BasicPageForMenu>);
        }
    }
}
