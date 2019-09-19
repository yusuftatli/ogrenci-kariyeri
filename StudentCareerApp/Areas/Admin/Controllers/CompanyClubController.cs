using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Common;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyClubController : Controller
    {

        private readonly ICompanyClubManager _companyClubManager;

        public CompanyClubController(ICompanyClubManager companyClubManager)
        {
            _companyClubManager = companyClubManager;
        }

        public IActionResult Company()
        {
            return View();
        }

      

       

        public async Task<JsonResult> AddOrUpdateCompany([FromBody]CompanyClubsDto model)
        {
            var res = await _companyClubManager.CreateCompanyClubs(model, HttpContext.GetSessionData<UserSession>("userInfo"));
            return Json(res);
        }

        #region Announcement
        public PartialViewResult CompanyAnnouncements(string seoUrl)
        {
            ViewBag.SeoUrl = seoUrl;
            return PartialView();
        }

        public async Task<JsonResult> GetCompanyAnnouncements(string seoUrl)
        {
            var res = await _companyClubManager.GetCompanyAnnouncements(seoUrl);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> AddOrUpdateCompanyAnnouncement(AnouncementDto model)
        {
            var res = await _companyClubManager.AddOrUpdateAnnouncement(model);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCompanyAnnouncement(long id)
        {
            var res = await _companyClubManager.DeleteCompanyAnnouncement(id);
            return Json(res);
        }
        #endregion

        #region Youtube Playlist
        public PartialViewResult CompanyYoutubePlaylist(string seoUrl)
        {
            ViewBag.SeoUrl = seoUrl;
            return PartialView();
        }

        public async Task<JsonResult> GetCompanyYoutubePlaylist(string seoUrl)
        {
            var res = await _companyClubManager.GetCompanyYoutubePlayList(seoUrl);
            return Json(res);
        }

        public async Task<JsonResult> AddOrUpdateCompanyYoutubePlaylist(YoutubeVideo model)
        {
            var res = await _companyClubManager.AddOrUpdateYoutubePlaylist(model);
            return Json(res);
        }

        public async Task<JsonResult> DeleteCompanyYoutubePlaylistItem(long id)
        {
            var res = await _companyClubManager.DeleteYoutubePlaylistItem(id);
            return Json(res);
        }
        #endregion


        [HttpGet("Clubs")]
        public IActionResult Clubs()
        {
            return View();
        }

    }
}