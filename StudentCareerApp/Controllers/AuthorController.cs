using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SCA.UI.Controllers
{
    public class AuthorController : Controller
    {
        [Route("editor")]
        public IActionResult Editor()
        {
            return View();
        }

        [Route("firma")]
        public IActionResult Agent()
        {
            return View();
        }
    }
}