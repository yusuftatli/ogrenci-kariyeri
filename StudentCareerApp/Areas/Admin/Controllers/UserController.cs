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
    [Route("[area]/[controller]")]
    public class UserController : Controller
    {
        // GET: User
        [HttpGet("UserList")]
        public IActionResult UserList()
        {
            ViewBag.showColumn = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")).RoleTypeId;
            return View();
        }

        [HttpGet("CreateUser")]
        public IActionResult CreateUser()
        {
            ViewBag.showColumn = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")).RoleTypeId;
            return View();
        }

        [HttpGet("UserDetailList")]
        public IActionResult UserDetailList()
        {
            return View();
        }
    }
}