﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCA.UI.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}