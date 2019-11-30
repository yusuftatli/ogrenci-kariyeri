using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Entity.Entities;
using SCA.Entity.Model;

namespace SCA.UI.Controllers
{
    public class InformationController : Controller
    {
        private readonly IBasicPagesService<Entity.Entities.BasicPages> _basicPageService;

        public InformationController(IBasicPagesService<Entity.Entities.BasicPages> basicPageService)
        {
            _basicPageService = basicPageService;
        }

        [Route("hakkimizda"), HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [Route("iletisim"), HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("bilgi/{basicSeoUrl}")]
        public async Task<IActionResult> BasicPage(string basicSeoUrl)
        {
            var res = await _basicPageService.GetByWhereParams<BasicPageVM>(x => x.SeoUrl == basicSeoUrl);
            return View((res.Data as List<BasicPageVM>).SingleOrDefault());
        }

    }
}