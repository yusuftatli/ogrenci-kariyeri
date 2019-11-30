using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Entity.Dto;
using SCA.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.Menu
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ICategoryService<Category> _categoryService;

        public MenuViewComponent(ICategoryService<Category> categoryService) => _categoryService = categoryService;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = await _categoryService.GetByWhereParams<MainCategoryDto>(x => x.IsActive == true);
            return View("_HeaderMenu", res.Data as List<MainCategoryDto>);
        }
    }
}
