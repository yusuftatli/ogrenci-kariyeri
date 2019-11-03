using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Entity.DTO;
using SCA.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.CommentViewComponent
{
    public class CommentViewComponent : ViewComponent
    {
        private readonly ICommentService<Comments> _commentService;

        public CommentViewComponent(ICommentService<Comments> commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long articleId)
        {
            var @params = new
            {
                articleId
            };
            var res = await _commentService.SPQueryAsync<object, CommentForUIDto>(@params, "GetUserComments");
            return View("_Comment", res.Data as List<CommentForUIDto>);
        }
    }
}
