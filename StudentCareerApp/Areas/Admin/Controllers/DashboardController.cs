using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Minimal()
        {
            return View();
        }
        public IActionResult Analytical()
        {
            return View();
        }
        public IActionResult Demographical()
        {
            return View();
        }
        public IActionResult Modern()
        {
            return View();
        }
    }
}