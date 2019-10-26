using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class UserController : Controller
    {
        // GET: User
        [HttpGet("UserList")]
        public IActionResult UserList()
        {
            var value = HttpContext.Session.GetString("userInfo");
            if (value != null)
            {
                ViewBag.showColumn = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")).RoleTypeId;

                return View();
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [HttpGet("CreateUser")]
        public IActionResult CreateUser()
        {
            var value = HttpContext.Session.GetString("userInfo");
            if (value != null)
            {
                ViewBag.showColumn = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")).RoleTypeId;

                return View();
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        [HttpGet("UserDetailList")]
        public IActionResult UserDetailList()
        {
            return View();
        }


        [HttpGet("Profile")]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet("Exit")]
        public IActionResult Exit()
        {
            HttpContext.Session.Remove("userInfo");
            return Redirect("/Home/Index");
        }
    }
}