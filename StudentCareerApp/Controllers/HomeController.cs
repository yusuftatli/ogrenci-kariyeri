using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services;
using SCA.Services.Interface;

namespace SCA.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        #region INTERFACES & CONSTRUCTOR
        private readonly IContentManager _contentManager;
        public HomeController(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }
        #endregion

        #region VIEWS
        public IActionResult Index()
        {
            return View();
        }
        #endregion

    }
}