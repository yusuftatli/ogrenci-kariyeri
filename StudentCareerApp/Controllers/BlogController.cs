using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services;
using SCA.Common;
using SCA.BLLServices;
using SCA.Entity.Entities;

namespace SCA.UI.Controllers
{
    public class BlogController : Controller
    {
        #region INTERFACES & CONSTRUCTOR
        private readonly IContentManager _contentManager;
        private readonly ICompanyClubManager _companyManager;
        private readonly ICommentManager _commentManager;
        private readonly ICommentService<Comments> _commentService;
        public BlogController(IContentManager contentManager, ICommentManager commentManager, ICommentService<Comments> commentService, ICompanyClubManager companyManager)
        {
            _contentManager = contentManager;
            _commentManager = commentManager;
            _commentService = commentService;
            _companyManager = companyManager;
        }
        #endregion


        [Route("haber/{SeoUrl}"), HttpGet]
        public async Task<IActionResult> Index(string seoUrl)
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();
            var guidBrowserId = HttpContext.Request.Cookies["okgdy"].ToString();
            var res = await _contentManager.GetContentUI(seoUrl, HttpContext.GetSessionData<UserSession>("userInfo")?.Id, ip: guidBrowserId);
            HttpContext.Session.SetString("ContentID", res.ContentId.ToString());
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

