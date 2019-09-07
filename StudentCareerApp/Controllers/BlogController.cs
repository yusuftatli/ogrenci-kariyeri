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
    public class BlogController : Controller
    {
        #region INTERFACES & CONSTRUCTOR
        private readonly IContentManager _contentManager;
        public BlogController(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }
        #endregion


        [Route("haber/{SeoUrl}"), HttpGet]
        public async Task<IActionResult> Index(string seoUrl)
        {
            var res = await _contentManager.GetContentUI(seoUrl);
            return View(res);
        }

    }
}

