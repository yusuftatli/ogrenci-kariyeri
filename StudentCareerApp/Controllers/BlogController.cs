using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SCA.Entity.DTO;
using SCA.Services;
using SCA.Services.Interface;
using SCA.Common;

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
            var res = await _contentManager.GetContentUI(seoUrl, HttpContext.GetSessionData<UserSession>("userInfo").Id);
            return View(res);
        }

    }
}

