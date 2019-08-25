using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class ContentController : Controller
    {
        [HttpGet("Assay")]
        public IActionResult Assay()
        {
            HttpContext.Session.SetString("name", "Jignesh Trivedi");
            ViewBag.name= HttpContext.Session.GetString("UserId");
            return View();
        }

        [HttpGet("AssayConfirm")]
        public IActionResult AssayConfirm()
        {
            return View();
        }

        [HttpGet("Question")]
        public IActionResult Question()
        {
            return View();
        }
    }
}