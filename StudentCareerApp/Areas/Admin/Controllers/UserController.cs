using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCA.UI.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class UserController : Controller
    {
        // GET: User
        [HttpGet("UserList")]
        public IActionResult UserList()
        {
            return View();
        }

        [HttpGet("CreateUser")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpGet("UserDetailList")]
        public IActionResult UserDetailList()
        {
            return View();
        }
    }
}