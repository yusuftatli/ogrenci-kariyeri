using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SCA.UI.Controllers
{
    public class InformationController : Controller
    {
        [Route("hakkimizda"), HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [Route("iletisim"), HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

    }
}