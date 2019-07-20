using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class CommentsController : ControllerBase
    {
        ICommentInterface _commentManager;
        public CommentsController(ICommentInterface commentManager)
        {
            _commentManager = commentManager;
        }

        /// <summary>
        /// makale veya test için yorum insert yapar 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Route("comment-CreateComments")]
        public async Task<ServiceResult> CreateComments([FromBody] CommentsDto dto)
        {
            return await _commentManager.CreateComments(dto);
        }

        /// <summary>
        /// onayda beykeleyn bütün yorumları çeker
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("comment-GetAllCommentsPendingApproval")]
        public async Task<ServiceResult> GetAllCommentsPendingApproval()
        {
            return await _commentManager.GetAllCommentsPendingApproval();
        }

        /// <summary>
        /// makaleye yazılmış olan yorumu admin tarafından onaylar
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost, Route("comment-ApproveComment")]
        public async Task<ServiceResult> ApproveComment(long Id)
        {
            return await _commentManager.ApproveComment(Id);
        }

        /// <summary>
        /// Makale ye ait tüm yorumları getitir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("comment-GetCommentForContent")]
        public List<CommentsDto> GetCommentForContent(long Id)
        {
            return _commentManager.GetCommentForContent(Id);
        }

        /// <summary>
        /// Makale yorumunu pasif yapar
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost, Route("comment-PassiveApproveComment")]
        public async Task<ServiceResult> PassiveApproveComment(long Id)
        {
            return await _commentManager.PassiveApproveComment(Id);
        }
    }
}