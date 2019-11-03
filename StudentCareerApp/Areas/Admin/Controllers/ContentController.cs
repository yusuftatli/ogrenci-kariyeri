using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContentController : Controller
    {
        public IActionResult Assay()
        {
            var value = HttpContext.Session.GetString("userInfo");
            if (value!=null)
            {
                ViewBag.showColumn = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")).RoleTypeId;

                return View();
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        public IActionResult AssayConfirm()
        {
            return View();
        }

        public IActionResult Question()
        {
            return View();
        }
        
    }
}