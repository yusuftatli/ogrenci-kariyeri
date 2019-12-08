using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SCA.BLLServices;
using SCA.Common;
using SCA.Entity.Dto;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Entity.Model.User;
using SCA.Services;

namespace SCA.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class SharedController : Controller
    {

        private readonly IUserManager _userManager;
        private readonly IUserService<Entity.Entities.Users> _userService;
        private ISender _sender;

        public SharedController(IUserManager userManager, IUserService<Entity.Entities.Users> userService, ISender sender)
        {
            _userService = userService;
            _userManager = userManager;
            _sender = sender;
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

        public PartialViewResult _ForgetPasswordPage()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<ActionResult> ChangePassword(string forgetAuthorizationKey)
        {
            var user = await _userService.GetByWhereParams<UserSummary>(x => x.ForgetAuthorizationKey == forgetAuthorizationKey && x.ForgetAuthorizationExpiryDate > DateTime.Now);
            if (user.Data.Count > 0)
                return View(user.Data.First());
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<JsonResult> ChangeForgottenPassword()
        {
            if (Request.Form["Password"].ToString() == Request.Form["RetypePassword"])
            {
                var model = new UserChangePassword
                {
                    Id = long.Parse(Request.Form["Id"].ToString()),
                    ForgetAuthorizationKey = Request.Form["ForgetAuthorizationKey"],
                    Password = Request.Form["Password"].ToString().MD5Hash()
                };
                var res = await _userService.UpdateAsync(model);
                return Json(res);
            }
            else
                return Json(Common.Result.Result.ReturnAsFail("Şifre uyumlu değil."));

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

        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> ForgetPassword(string email)
        {
            var users = await _userService.GetByWhereParams<UserSummary>(x => x.EmailAddress == email);
            if (users.Data != null)
            {
                var user = users.Data as List<UserSummary>;
                var forgetModel = new UserForgetPassword
                {
                    ForgetAuthorizationExpiryDate = DateTime.Now.AddMinutes(30),
                    Id = user.First().Id,
                    ForgetAuthorizationKey = Guid.NewGuid().ToString()
                };
                await _userService.UpdateAsync(forgetModel);

                EmailSettings emailSetting = await _sender.GetEmailSetting("PASSRENEW");
                EmailsDto emailData = new EmailsDto
                {
                    Body = $"Aşağıdaki linkten email değiştirme işlemini gerçekleştirebilirsiniz.\n\nhttps://localhost:44308/shared/changepassword/{forgetModel.ForgetAuthorizationKey}",
                    Subject = "Öğrenci Kariyer Şifre Yenileme",
                    ToEmail = email,
                    IsSend = false,
                    UserId = forgetModel.Id,
                    SendDate = DateTime.Now,
                    CcEmail = "",
                    FromEmail = emailSetting.UsernameEmail,
                    Process = "Şifre Yenileme"
                };
                var res = await _sender.SaveEmails(emailData);
                return Json(res);
            }
            else
                return Json(Common.Result.Result.ReturnAsFail("Yanlış email adresi"));

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
        public async Task<IActionResult> Register()
        {
            if (Request.Form["Password"].ToString() == Request.Form["RetypePassword"])
            {
                var educationType = (Entity.Enums.EducationType)Enum.Parse(typeof(Entity.Enums.EducationType), Request.Form["EducationType"].ToString());
                var model = new UserRegisterDto
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
                    Password = Request.Form["Password"].ToString().MD5Hash(),
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