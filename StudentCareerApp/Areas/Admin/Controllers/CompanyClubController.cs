using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Common;
using SCA.Entity.DTO;
using SCA.Entity.Entities;
using SCA.Services;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyClubController : Controller
    {

        private readonly ICompanyClubManager _companyClubManager;
        private readonly IAnnounsmentService<Announsment> _announsmentService;
        private readonly IYoutubePlaylistService<YoutubePlaylist> _youtubePlaylistService;
        private readonly ICompanyClubService<CompanyClubs> _companyClubService;

        public CompanyClubController(   ICompanyClubManager companyClubManager, 
                                        IAnnounsmentService<Announsment> announsmentService,
                                        IYoutubePlaylistService<YoutubePlaylist> youtubePlaylistService,
                                        ICompanyClubService<CompanyClubs> companyClubService)
        {
            _announsmentService = announsmentService;
            _youtubePlaylistService = youtubePlaylistService;
            _companyClubService = companyClubService;
            _companyClubManager = companyClubManager;
        }

        public IActionResult Company()
        {
            return View();
        }

      

       

        public async Task<JsonResult> AddOrUpdateCompany([FromBody]CompanyClubsDto model)
        {
            var user = HttpContext.GetSessionData<UserSession>("userInfo");
            if(model.Id.Equals(0))
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedUserId = user.Id;
                model.CreateUserName = user.Name + " " + user.Surname;
                await _companyClubService.InsertAsync(model);
            }
            else
            {
                model
            }
            var res = await _companyClubManager.CreateCompanyClubs(model, HttpContext.GetSessionData<UserSession>("userInfo"));
            return Json(res);
        }

        #region Announcement
        public PartialViewResult CompanyAnnouncements(int companyId, string seoUrl)
        {
            ViewBag.CompanyId = companyId;
            ViewBag.SeoUrl = seoUrl;
            return PartialView();
        }

        public async Task<JsonResult> GetCompanyAnnouncements(string seoUrl)
        {
            var @params = new
            {
                seoUrl
            };
            var res = await _announsmentService.SPQueryAsync<object, SCA.Entity.SPModels.SPResult.GetCompanyAnnouncements>(@params);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> AddOrUpdateCompanyAnnouncement(AnouncementDto model)
        {
            var res = model.Id > 0 ? await _announsmentService.UpdateAsync(model) : await _announsmentService.InsertAsync(model);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCompanyAnnouncement(long id)
        {
            var res = await _announsmentService.DeleteAsync(id);
            return Json(res);
        }
        #endregion

        #region Youtube Playlist
        public PartialViewResult CompanyYoutubePlaylist(long companyId, string seoUrl)
        {
            ViewBag.CompanyId = companyId;
            ViewBag.SeoUrl = seoUrl;
            return PartialView();
        }

        public async Task<JsonResult> GetCompanyYoutubePlaylist(string seoUrl)
        {
            var @params = new { seoUrl = seoUrl};
            var res = await _youtubePlaylistService.SPQueryAsync<object, SCA.Entity.SPModels.SPResult.GetCompanyYoutubePlaylist>(@params);
            return Json(res);
        }

        public async Task<JsonResult> AddOrUpdateCompanyYoutubePlaylist(YoutubeVideo model)
        {
            var res = model.Id > 0 ? await _youtubePlaylistService.UpdateAsync(model) : await _youtubePlaylistService.InsertAsync(model);
            return Json(res);
        }

        public async Task<JsonResult> DeleteCompanyYoutubePlaylistItem(long id)
        {
            var res = await _youtubePlaylistService.DeleteAsync(id);
            return Json(res);
        }
        #endregion


        public IActionResult Clubs()
        {
            return View();
        }

    }
}