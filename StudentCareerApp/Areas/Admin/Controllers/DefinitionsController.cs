using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCA.UI.Controllers
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

        [HttpGet("SchollManager")]
        public IActionResult SchollManager()
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