using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Common;
using SCA.Entity.DTO;
using SCA.Entity.Entities;
using SCA.Entity.Model;
using System;
using System.Threading.Tasks;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class PagesController : Controller
    {
        private readonly IBasicPagesService<SCA.Entity.Entities.BasicPages> _basicPagesService;

        public PagesController(IBasicPagesService<SCA.Entity.Entities.BasicPages> basicPagesService)
        {
            _basicPagesService = basicPagesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetBasicPageList()
        {
            var res = await _basicPagesService.GetByWhereParams<SCA.Entity.Model.BasicPageList>(x => x.IsDeleted == false);
            return Json(res);
        }

        public async Task<JsonResult> AddOrUpdateBasicPage(SCA.Entity.Model.BasicPageVM model)
        {
            var user = HttpContext.GetSessionData<UserSession>("userInfo");

            if (model.Id.Equals(0))
            {
                var reqModel = new BasicPageInsertModel
                {
                    CreatedDate = DateTime.Now,
                    CreatedUserId = user.Id,
                    Description = model.Description,
                    ImagePath = model.ImagePath,
                    IsActive = model.IsActive,
                    SeoUrl = model.SeoUrl,
                    Title = model.Title
                };
                var res = await _basicPagesService.InsertAsync(reqModel);
                return Json(res);
            }
            else
            {
                var reqModel = new BasicPageUpdateModel
                {
                    Description = model.Description,
                    Id = model.Id,
                    ImagePath = model.ImagePath,
                    IsActive = model.IsActive,
                    SeoUrl = model.SeoUrl,
                    Title = model.Title,
                    UpdatedDate = DateTime.Now,
                    UpdatedUserID = user.Id
                };
                var res = await _basicPagesService.UpdateAsync(reqModel);
                return Json(res);
            }
        }

        public async Task<JsonResult> SoftDeleteBasicPage(long id)
        {
            var user = HttpContext.GetSessionData<UserSession>("userInfo");

            var @params = new
            {
                Id = id,
                DeletedDate = DateTime.Now,
                DeletedUserId = user.Id
            };
            var res = await _basicPagesService.UpdateAsync(@params);
            return Json(res);
        }
        
    }
}