using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Common;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Services;

namespace StudentCareerApp.Controllers
{
    public class UserController : Controller
    {

        private readonly ICategoryManager _categoryManager;
        private readonly IUserManager _userManager;
        private readonly IUserService<SCA.Entity.Entities.Users> _userService;
        private readonly IDefinitionManager _definitionManager;
        private readonly IAddressManager _addressManager;

        public UserController(ICategoryManager categoryManager, IUserManager userManager, IUserService<SCA.Entity.Entities.Users> userService, IDefinitionManager definitionManager, IAddressManager addressManager)
        {
            _categoryManager = categoryManager;
            _userManager = userManager;
            _userService = userService;
            _definitionManager = definitionManager;
            _addressManager = addressManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.GetSessionData<UserSession>("userInfo")?.Id;
            if(userId.HasValue)
            {
                var user = await _userService.GetByIdAsync<SCA.Entity.Entities.Users>(userId.Value);
                var model = new AllUniversityInformationDto
                {
                    Universities = await _definitionManager.GetUniversityForUI(),
                    Faculties = await _definitionManager.GetFacultyForUI(),
                    Departments = await _definitionManager.GetDepartmentForUI(),
                    Classes = await _definitionManager.GetStudentClassForUI(),
                    HighSchools = await _definitionManager.GetHighSchoolForUI(),
                    Cities = await _addressManager.CityList()
                };  
                var returnModel = new UserWithAllUniversityInformationDTO
                {
                    Definitions = model,
                    User = user.Data
                };
                return View(returnModel);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SetCategories()
        {
            return View();
        }

        public async Task<IActionResult> ChangeProfile()
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