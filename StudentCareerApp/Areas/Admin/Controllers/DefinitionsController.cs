using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class DefinitionsController : Controller
    {
        // GET: Definitions
        [HttpGet("Category")]
        public IActionResult Category()
        {
            return View();
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("RoleType")]
        public IActionResult RoleType()
        {
            return View();
        }

    }

}