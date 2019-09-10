using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Common;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class CompanyClubController : Controller
    {

        private readonly ICompanyClubManager _companyClubManager;

        public CompanyClubController(ICompanyClubManager companyClubManager)
        {
            _companyClubManager = companyClubManager;
        }

        [HttpGet("Company")]
        public IActionResult Company()
        {
            return View();
        }

        public async Task<JsonResult> AddOrUpdateCompany(CompanyClubsDto model)
        {
            var res = await _companyClubManager.CreateCompanyClubs(model, HttpContext.GetSessionData<UserSession>("userInfo"));
            return Json(res);
        }

        [HttpGet("Clubs")]
        public IActionResult Clubs()
        {
            return View();
        }

    }
}