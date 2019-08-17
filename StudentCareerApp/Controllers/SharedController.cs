using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public SharedController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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
        public async Task<IActionResult> Register(UserRegisterDto model)
        {
            
            if(Request.Form["Password"].ToString() == Request.Form["RetypePassword"])
            {
                var educationType = (Entity.Enums.EducationType)Enum.Parse(typeof(Entity.Enums.EducationType), Request.Form["EducationType"].ToString());
                model = new UserRegisterDto
                {
                    BirthDate = Convert.ToDateTime(Request.Form["BirthDate"].ToString()),
                    ClassId = Request.Form["IsStudent"].ToString() == "on" ? long.Parse(Request.Form["ClassId"].ToString()) : 0,
                    DepartmentId = (educationType == Entity.Enums.EducationType.University ||educationType == Entity.Enums.EducationType.Master) ? long.Parse(Request.Form["DepartmentId"].ToString()) : 0,
                    EducationStatusId = educationType,
                    EmailAddress = Request.Form["EmailAddress"].ToString(),
                    FacultyId = (educationType == Entity.Enums.EducationType.University || educationType == Entity.Enums.EducationType.Master) ? int.Parse(Request.Form["FacultyId"].ToString()) : 0,
                    GenderId = (Entity.Enums.GenderType)Enum.Parse(typeof(Entity.Enums.GenderType), Request.Form["GenderId"].ToString()),
                    HighSchoolTypeId = (educationType == Entity.Enums.EducationType.HighSchool) ? int.Parse(Request.Form["HighSchoolTypeId"].ToString()) : 0,
                    IsStudent = Request.Form["IsStudent"].ToString() == "on",
                    Name = Request.Form["Name"].ToString(),
                    Password = Request.Form["Password"].ToString(),
                    Surname = Request.Form["Surname"].ToString(),
                    UniversityId = (educationType == Entity.Enums.EducationType.University || educationType == Entity.Enums.EducationType.Master) ? int.Parse(Request.Form["UniversityId"].ToString()) : 0,
                    UserName = Request.Form["Username"].ToString(),
                    SubscribeNewsletter = Request.Form["SubscribeNewsletter"].ToString() == "on",
                    ReferanceCode = Request.Form["ReferanceCode"].ToString()
                };
                try
                {
                    var a = _mapper.Map<UsersDTO>(model);
                    var res = await _userManager.CreateUser(_mapper.Map<UsersDTO>(model));
                }
                catch(Exception ex)
                {
                    var a = ex;
                }
                return View();
            }
            _userManager.CreateUser()
            return View();
        }

    }
}