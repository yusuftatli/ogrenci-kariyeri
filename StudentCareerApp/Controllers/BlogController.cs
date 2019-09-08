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
        private readonly ICommentManager _commentManager;
        public BlogController(IContentManager contentManager, ICommentManager commentManager)
        {
            _contentManager = contentManager;
            _commentManager = commentManager;
        }
        #endregion


        [Route("{SeoUrl}"), HttpGet]
        public async Task<IActionResult> Index(string seoUrl)
        {
            var res = await _contentManager.GetContentUI(seoUrl, HttpContext.GetSessionData<UserSession>("userInfo")?.Id);
            return View(res);
        }

        [HttpPost]
        public async Task<JsonResult> AddOrRemoveFavorite(FavoriteDto model)
        {
            if (HttpContext.GetSessionData<UserSession>("userInfo")?.Id > 0)
            {
                model.UserId = HttpContext.GetSessionData<UserSession>("userInfo").Id;
                var res = await _contentManager.CreateFavorite(model);
                return Json(new { Status = res, Explanation = "Beklenmedik bir hata oluştu." });
            }

            return Json(new { Status = false, Explanation = "Giriş yapılmadan yorum yapılamaz." });
        }

        [HttpPost]
        public async Task<JsonResult> PostComment(CommentForUIDto model)
        {
            model.UserID = HttpContext.GetSessionData<UserSession>("userInfo").Id;
            var res = await _commentManager.CreateComments(model);
            return Json(res);
        }

    }
}

