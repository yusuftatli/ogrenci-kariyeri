using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("Company")]
        public IActionResult Company()
        {
            return View();
        }

        [HttpGet("Clubs")]
        public IActionResult Clubs()
        {
            return View();
        }

    }
}