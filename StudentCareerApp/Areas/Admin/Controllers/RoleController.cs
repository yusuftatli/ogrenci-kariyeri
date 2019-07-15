using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCA.UI.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class RoleController : Controller
    {
        // GET: Role
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}