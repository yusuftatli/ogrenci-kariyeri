using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Entity.DTO;
using SCA.Entity.Entities;
using SCA.Entity.SPModels.SPResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.SidebarComponents.PopularCategories
{
    public class PopularCategoriesViewComponent : ViewComponent
    {

        private readonly ICategoryService<Category> _categoryService;

        public PopularCategoriesViewComponent(ICategoryService<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = await _categoryService.SPQueryAsync<object, GetCategoryUsings>(null);
            return View("_PopularCategories", res.Data as List<GetCategoryUsings>);
        }
    }
}
