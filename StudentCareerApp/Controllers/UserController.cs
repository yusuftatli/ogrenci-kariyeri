using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Common;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Entity.Model.User;
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
        private readonly ISocialMediaService<SCA.Entity.Entities.SocialMedia> _socialMediaService;

        public UserController(ICategoryManager categoryManager, IUserManager userManager, IUserService<SCA.Entity.Entities.Users> userService, IDefinitionManager definitionManager, IAddressManager addressManager, ISocialMediaService<SCA.Entity.Entities.SocialMedia> socialMediaService)
        {
            _categoryManager = categoryManager;
            _userManager = userManager;
            _userService = userService;
            _definitionManager = definitionManager;
            _addressManager = addressManager;
            _socialMediaService = socialMediaService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.GetSessionData<UserSession>("userInfo")?.Id;
            if (userId.HasValue)
            {
                var user = await _userService.GetByIdAsync<UserProfile>(userId.Value);
                var socialMedia = await _socialMediaService.GetByWhereParams<SocialMediaDto>(x => x.UserId == userId.Value);
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
                    User = user.Data,
                    SocialMedias = socialMedia.Data
                };
                return View(returnModel);
            }
            return RedirectToAction("Index", "Home");
        }

        //TODO: foreachler düzeltilicek.
        [HttpPost]
        public async Task<IActionResult> UpdateUserProfile(UserProfileVM model)
        {
            await _userService.UpdateAsync(model.UserProfile);
            var socialMedias = await _socialMediaService.GetByWhereParams<SCA.Entity.Entities.SocialMedia>(x => x.UserId == model.UserProfile.Id);
            var socialMediaIds = socialMedias.Data.Select(x => x.Id ).ToArray();
            foreach(var x in socialMediaIds)
                await _socialMediaService.DeleteAsync(x);
            foreach (var x in model.SocialMedias)
                await _socialMediaService.InsertAsync(x);
            return Ok();
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
                var res = await _userManager.UpdateUserCategory( categories,"");
                return Json(res);
            }
            return Json(new { resultCode = 401, message = "Giriş yapılmadan ilgi alanları seçilemez." });
        }
    }
}