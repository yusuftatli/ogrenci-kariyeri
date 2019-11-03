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
    public class CommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}