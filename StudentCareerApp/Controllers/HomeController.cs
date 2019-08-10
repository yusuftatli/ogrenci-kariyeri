using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services.Interface;

namespace SCA.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        #region INTERFACES & CONSTRUCTOR
        private readonly IB2CManagerUI _b2cManager;
        public HomeController(IB2CManagerUI b2cManager)
        {
            _b2cManager = b2cManager;
        }
        #endregion

        #region VIEWS
        public IActionResult Index()
        {
            var res = _b2cManager.GetContentsForHomePage().Result;
            //var fakeData = FakeContentList();
            return View();
        }
        #endregion

    }
}