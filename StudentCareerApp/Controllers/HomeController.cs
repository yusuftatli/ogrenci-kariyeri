using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Services;
using SCA.Services.Interface;

namespace SCA.UI.Controllers
{
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

        public async Task<IActionResult> GetNewsWith2ColumnsData(int count, int offset)
        {
            var res = await _contentManager.GetContentForHomePage(HitTypes.LastAssay, count, offset);
            return View("~/Views/Shared/Components/NewsWith2Columns/_NewsWith2Columns.cshtml", res);
        }
        #endregion

    }
}