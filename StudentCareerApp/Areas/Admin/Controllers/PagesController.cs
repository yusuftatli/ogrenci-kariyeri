using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class PagesController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}