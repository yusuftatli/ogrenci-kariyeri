using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Services;

namespace StudentCareerApp.Controllers
{
    public class UserController : Controller
    {

        private readonly ICategoryManager _categoryManager;

        public UserController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
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
            var cats = categories.Split(",");
            return Json(cats);
        }
    }
}