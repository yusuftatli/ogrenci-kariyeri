using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Common;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Entities;
using SCA.Entity.Model;
using SCA.Entity.Model.CompanyClub;
using SCA.Entity.Model.ImageGaleries;
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
        private readonly IImageGaleryService<SCA.Entity.Entities.ImageGalery> _imageGaleryService;
        private IHostingEnvironment _hostingEnvironment;

        public CompanyClubController(
                                        ICompanyClubManager companyClubManager,
                                        IAnnounsmentService<Announsment> announsmentService,
                                        IYoutubePlaylistService<YoutubePlaylist> youtubePlaylistService,
                                        ICompanyClubService<SCA.Entity.Entities.CompanyClubs> companyClubService,
                                        ISocialMediaService<SCA.Entity.Entities.SocialMedia> socialMediaService,
                                        IImageGaleryService<SCA.Entity.Entities.ImageGalery> imageGaleryService,
                                        IHostingEnvironment hostingEnvironment)
        {
            _announsmentService = announsmentService;
            _youtubePlaylistService = youtubePlaylistService;
            _companyClubService = companyClubService;
            _companyClubManager = companyClubManager;
            _socialMediaService = socialMediaService;
            _imageGaleryService = imageGaleryService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Company()
        {
            return View();
        }

        #region Company
        public PartialViewResult CompanyPage(long? id = null)
        {
            ViewBag.CompanyId = id;
            return PartialView();
        }

        public async Task<JsonResult> GetCompanyDetails(long id)
        {
            var res = await _companyClubService.GetByIdAsync<CompanyVM>(id);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCompany(long id)
        {
            var user = HttpContext.GetSessionData<UserSession>("userInfo");
            var @params = new
            {
                Id = id,
                DeletedUserId = user.Id,
                DeletedDate = DateTime.Now
            };
            var res = await _companyClubService.UpdateAsync(@params);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> AddOrUpdateCompany(CompanyClubsDto model)
        {
            var user = HttpContext.GetSessionData<UserSession>("userInfo");
            var res = new ServiceResult();
            //for insert model
            if (model.Id.Equals(0))
            {
                var requestModel = new CompanyClubInsertModel
                {
                    CompanyClupType = SCA.Entity.Enums.CompanyClupType.Company,
                    CreatedUserId = user.Id,
                    CreateUserName = user.Name,
                    Description = model.Description,
                    EmailAddress = model.EmailAddress,
                    HeaderImage = model.HeaderImage,
                    ImageDirectory = model.ImageDirectory,
                    PhoneNumber = model.PhoneNumber,
                    SectorId = model.SectorId,
                    SectorType = model.SectorType,
                    SeoUrl = model.SeoUrl,
                    ShortName = model.ShortName,
                    WebSite = model.WebSite
                };
                res = await _companyClubService.InsertAsync(requestModel);
            }
            //for update model
            else
            {
                var requestModel = new CompanyClubUpdateModel
                {
                    UpdatedUserId = user.Id,
                    CompanyClupType = SCA.Entity.Enums.CompanyClupType.Company,
                    Description = model.Description,
                    EmailAddress = model.EmailAddress,
                    Id = model.Id,
                    ImageDirectory = model.ImageDirectory,
                    PhoneNumber = model.PhoneNumber,
                    SectorId = model.SectorId,
                    SectorType = model.SectorType,
                    SeoUrl = model.SeoUrl,
                    ShortName = model.ShortName,
                    WebSite = model.WebSite,
                    HeaderImage = model.HeaderImage
                };
                res = await _companyClubService.UpdateAsync(requestModel);
            }
            return Json(res);
        }
        #endregion

        #region ImageGalery
        public PartialViewResult CompanyImages(long companyId, string companyName)
        {
            ViewBag.CompanyId = companyId;
            ViewBag.CompanyName = companyName;
            return PartialView();
        }

        public async Task<JsonResult> GetCompanyImages(long companyId)
        {
            var res = await _imageGaleryService.GetByWhereParams<ImageGaleryDto>(x => x.IsActive == true && x.CompanyClubId == companyId);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> AddCompanyImage(IFormFile file, long companyId, string companyName)
        {
            var serverPath = $"AdminFiles\\CMS\\Content\\Companies\\{companyId.ToString()}-{companyName}";
            string path = Path.Combine(_hostingEnvironment.WebRootPath, serverPath);
            Directory.CreateDirectory(path);

            if (file.Length > 0)
                using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    await file.CopyToAsync(fileStream);

            var model = new ImageGaleryInsertModel
            {
                CompanyClubId = companyId,
                ImagePath = "/"+serverPath.Replace("\\", "/") + "/" + file.FileName,
                IsActive = true
            };
            var res = await _imageGaleryService.InsertAsync(model);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCompanyImage(string path, long id)
        {
            var res = await _imageGaleryService.DeleteAsync(id);
            var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, path.TrimStart('/'));
            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            return Json(res);
        }
        #endregion

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
            var @params = new { seoUrl = seoUrl };
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
            var res = await _socialMediaService.GetByWhereParams<SocialMediaVM>(x => x.CompanyClupId == companyId);
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