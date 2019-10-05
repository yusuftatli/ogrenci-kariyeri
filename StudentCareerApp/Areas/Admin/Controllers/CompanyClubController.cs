using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Common;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Entities;
using SCA.Entity.Model;
using SCA.Entity.Model.SocialMedias;
using SCA.Services;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyClubController : Controller
    {

        private readonly ICompanyClubManager _companyClubManager;
        private readonly IAnnounsmentService<Announsment> _announsmentService;
        private readonly IYoutubePlaylistService<YoutubePlaylist> _youtubePlaylistService;
        private readonly ICompanyClubService<SCA.Entity.Entities.CompanyClubs> _companyClubService;
        private readonly ISocialMediaService<SCA.Entity.Entities.SocialMedia> _socialMediaService;
        private readonly IMapper _mapper;

        public CompanyClubController(   IMapper mapper,
                                        ICompanyClubManager companyClubManager, 
                                        IAnnounsmentService<Announsment> announsmentService,
                                        IYoutubePlaylistService<YoutubePlaylist> youtubePlaylistService,
                                        ICompanyClubService<SCA.Entity.Entities.CompanyClubs> companyClubService,
                                        ISocialMediaService<SCA.Entity.Entities.SocialMedia> socialMediaService)
        {
            _mapper = mapper;
            _announsmentService = announsmentService;
            _youtubePlaylistService = youtubePlaylistService;
            _companyClubService = companyClubService;
            _companyClubManager = companyClubManager;
            _socialMediaService = socialMediaService;
        }

        public IActionResult Company()
        {
            return View();
        }

        public PartialViewResult CompanyPage(long? id = null)
        {
            ViewBag.CompanyId = id;
            return PartialView();
        }

        public async Task<JsonResult> GetCompanyDetails(long id)
        {
            var res = await _companyClubService.GetByIdAsync<SocialMediaVM>(id);
            return Json(res);
        }

        public async Task<JsonResult> DeleteCompany(long id)
        {
            var res = await _companyClubService.DeleteAsync(id);
            return Json(res);
        }
       
        [HttpPost]
        public async Task<JsonResult> AddOrUpdateCompany([FromBody]CompanyClubsDto model)
        {
            var user = HttpContext.GetSessionData<UserSession>("userInfo");
            var res = new ServiceResult();
            //for insert model
            if(model.Id.Equals(0))
            {
                var requestModel = _mapper.Map<CompanyClubInsertModel>(model);
                requestModel.CreatedUserId = user.Id;
                requestModel.CreateUserName = user.Name;
                requestModel.CompanyClupType = SCA.Entity.Enums.CompanyClupType.Company;
                res = await _companyClubService.InsertAsync(requestModel);
            }
            //for 
            else
            {
                var requestModel = _mapper.Map<CompanyClubUpdateModel>(model);
                requestModel.UpdatedUserId = user.Id;
                requestModel.CompanyClupType = SCA.Entity.Enums.CompanyClupType.Company;
                res = await _companyClubService.UpdateAsync(requestModel);
            }
            //var res = await _companyClubManager.CreateCompanyClubs(model, HttpContext.GetSessionData<UserSession>("userInfo"));
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

        #region Social Media
        public PartialViewResult CompanySocialMedias(long companyId)
        {
            ViewBag.CompanyId = companyId;
            return PartialView();
        }

        public async Task<JsonResult> GetCompanySocialMedias(long companyId)
        {
            var res = await _socialMediaService.GetByWhereParams<SocialMediaVM>(x=>(int)x.SocialMediaType == (int)SCA.Entity.Enums.SocialMediaType.Instagram);
            return Json(res);
        }

        public async Task<JsonResult> AddOrUpdateCompanySocialMedia(SocialMediaVM model)
        {
            var res = model.Id > 0 ? await _socialMediaService.UpdateAsync(model) : await _socialMediaService.InsertAsync(model);
            return Json(res);
        }

        public async Task<JsonResult> DeleteSocialMedia(long id)
        {
            var res = await _socialMediaService.DeleteAsync(id);
            return Json(res);
        }
        #endregion


        public IActionResult Clubs()
        {
            return View();
        }

    }
}