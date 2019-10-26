using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.Anouncement
{
    public class MenuComponent : ViewComponent
    {
        private readonly IMenuManager _menuManager;
        public MenuComponent(IMenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<MenuDto> model = null)
        {
            var returnModel = await _menuManager.GetMenus(123);
            return View("_MenusScreen", returnModel as List<MenuDto>);
        }
    }
}
