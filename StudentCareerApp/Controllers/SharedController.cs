using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SCA.BLLServices;
using SCA.Entity.Dto;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Services;

namespace SCA.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class SharedController : Controller
    {

        private readonly IUserManager _userManager;

        public SharedController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult CookieEnable()
        {
            return PartialView();
        }


        public PartialViewResult _LoginPage()
        {
            return PartialView();
        }

        public PartialViewResult _RegisterPage()
        {
            return PartialView();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> Login(string email, string password)
        {
            var res = await _userManager.CheckUserForLogin(email, password);

            if (res.IsSuccess())
            {
                HttpContext.Session.SetString("userInfo", Newtonsoft.Json.JsonConvert.SerializeObject(res.Data));
                JsonSerializer serializer = new JsonSerializer();
                var result = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo"));
                HttpContext.Session.SetString("NameSurname", result.Name + " " + result.Surname);
                HttpContext.Session.SetString("RoleTypeId", result.RoleTypeId.ToString());
                return Json(res);
            }
            else
                return Json(res);
        }

        [HttpGet]
        public async Task<JsonResult> Logout()
        {
            await Task.Run(() =>
            {
                HttpContext.Session.Remove("userInfo");
            });
            return Json(new { message = "Çıkış yapıldı. Umarız tekrar dönersiniz :)" });

        }

        [ValidateAntiForgeryToken]
        [Route("Shared/Register"), HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            if (Request.Form["Password"].ToString() == Request.Form["RetypePassword"])
            {
                var educationType = (Entity.Enums.EducationType)Enum.Parse(typeof(Entity.Enums.EducationType), Request.Form["EducationType"].ToString());
                model = new UserRegisterDto
                {
                    BirthDate = Convert.ToDateTime(Request.Form["BirthDate"].ToString()),
                    ClassId = Request.Form["IsStudent"].ToString() == "on" ? long.Parse(Request.Form["ClassId"].ToString()) : 0,
                    DepartmentId = (educationType == Entity.Enums.EducationType.University || educationType == Entity.Enums.EducationType.Master) ? long.Parse(Request.Form["DepartmentId"].ToString()) : 0,
                    EducationStatusId = educationType,
                    EmailAddress = Request.Form["EmailAddress"].ToString(),
                    FacultyId = (educationType == Entity.Enums.EducationType.University || educationType == Entity.Enums.EducationType.Master) ? long.Parse(Request.Form["FacultyId"].ToString()) : 0,
                    GenderId = (Entity.Enums.GenderType)Enum.Parse(typeof(Entity.Enums.GenderType), Request.Form["GenderId"].ToString()),
                    HighSchoolTypeId = (educationType == Entity.Enums.EducationType.HighSchool) ? long.Parse(Request.Form["HighSchoolTypeId"].ToString()) : 0,
                    IsStudent = Request.Form["IsStudent"].ToString() == "on",
                    Name = Request.Form["Name"].ToString(),
                    Password = Request.Form["Password"].ToString(),
                    Surname = Request.Form["Surname"].ToString(),
                    UniversityId = (educationType == Entity.Enums.EducationType.University || educationType == Entity.Enums.EducationType.Master) ? long.Parse(Request.Form["UniversityId"].ToString()) : 0,
                    UserName = Request.Form["Username"].ToString(),
                    SubscribeNewsletter = Request.Form["SubscribeNewsletter"].ToString() == "on",
                    ReferanceCode = Request.Form["ReferanceCode"].ToString()
                };
                var res = await _userManager.RegisterUser(model);
                if (res.IsSuccess())
                {
                    var loginRes = await _userManager.CheckUserForLogin(model.EmailAddress, model.Password);
                    if (loginRes.IsSuccess())
                    {
                        HttpContext.Session.SetString("userInfo", Newtonsoft.Json.JsonConvert.SerializeObject(res.Data));
                        return RedirectToAction("SetCategories", "User");
                    }
                }

            }
            return RedirectToAction("Index", "Home");
        }

    }
}