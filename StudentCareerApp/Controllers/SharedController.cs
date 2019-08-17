using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [ValidateAntiForgeryToken]
        [Route("Shared/Register"), HttpPost]
        public IActionResult Register(UserRegisterDto model)
        {
            var a = 1;
            if (ModelState.IsValid)
            {
                var b = 1;
            }
            if(Request.Form["Password"].ToString() == Request.Form["RetypePassword"])
            {
                var educationType = (Entity.Enums.EducationType)Enum.Parse(typeof(Entity.Enums.EducationType), Request.Form["DepartmentId"].ToString());
                model = new UserRegisterDto
                {
                    BirthDate = Convert.ToDateTime(Request.Form["BirthDate"].ToString()),
                    ClassId = bool.Parse(Request.Form["IsStudent"].ToString()) ? long.Parse(Request.Form["ClassId"].ToString()) : 0,
                    DepartmentId = (educationType == Entity.Enums.EducationType.University ||educationType == Entity.Enums.EducationType.Master) ? long.Parse(Request.Form["DepartmentId"].ToString()) : 0,
                    EducationStatusId = educationType,
                    EmailAddress = Request.Form["EmailAddress"].ToString(),
                    FacultyId = (educationType == Entity.Enums.EducationType.University || educationType == Entity.Enums.EducationType.Master) ? int.Parse(Request.Form["FacultyId"].ToString()) : 0,
                    GenderId = (Entity.Enums.GenderType)Enum.Parse(typeof(Entity.Enums.GenderType), Request.Form["GenderId"].ToString()),
                    HighSchoolTypeId = (educationType == Entity.Enums.EducationType.HighSchool) ? int.Parse(Request.Form["HighSchoolTypeId"].ToString()) : 0,
                    IsStudent = bool.Parse(Request.Form["IsStudent"].ToString()),
                    Name = Request.Form["Name"].ToString(),
                    Password = Request.Form["Password"].ToString(),
                    Surname = Request.Form["Surname"].ToString(),
                    UniversityId = (educationType == Entity.Enums.EducationType.University || educationType == Entity.Enums.EducationType.Master) ? int.Parse(Request.Form["UniversityId"].ToString()) : 0,
                    Username = Request.Form["Username"].ToString()
                };
            }
            return View();
        }

    }
}