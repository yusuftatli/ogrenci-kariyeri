﻿using Microsoft.AspNetCore.Http;
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
    public class ContentController : Controller
    {
        [HttpGet("Assay")]
        public IActionResult Assay()
        {
            var data = HttpContext.Session.GetString("userInfo");
            //if (HttpContext.Session.GetString("userInfo") != null)
            //{
            //    CookieOptions options = new CookieOptions();
            //    options.Expires = DateTime.Now.AddDays(1);
            //    var result = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo"));
            //    Response.Cookies.Append("token", result.Token, options);
            //}
            ViewBag.showColumn = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")).RoleTypeId;
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