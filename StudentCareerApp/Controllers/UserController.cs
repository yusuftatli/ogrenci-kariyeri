using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Common;
using SCA.Entity.DTO;
using SCA.Services;

namespace StudentCareerApp.Controllers
{
    public class UserController : Controller
    {

        private readonly ICategoryManager _categoryManager;
        private readonly IUserManager _userManager;

        public UserController(ICategoryManager categoryManager, IUserManager userManager)
        {
            _categoryManager = categoryManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> SetCategories()
        {
            return View();
        }

        public async Task<JsonResult> GetCategories()
        {
            var res = await _categoryManager.MainCategoryListWithParents();
            return Json(res);
        }

        public async Task<JsonResult> PostCategories(string categories)
        {
            var userId = HttpContext.GetSessionData<UserSession>("userInfo")?.Id;
            if (userId != null)
            {
                var res = await _userManager.UpdateUserCategory(userId.Value, categories);
                return Json(res);
            }
            return Json(new { resultCode = 401, message = "Giriş yapılmadan ilgi alanları seçilemez."});
        }
    }
}