using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class ScreenController : Controller
    {
        [HttpGet("CreateRoleTypeforSuperAdmin")]
        public IActionResult CreateRoleTypeforSuperAdmin()
        {
            return View();
        }
    }
}