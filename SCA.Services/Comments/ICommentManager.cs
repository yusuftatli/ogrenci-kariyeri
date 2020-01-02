
using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface ICommentManager
    {
        /// <summary>
        /// makale veya test için yorum insert yapar 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ServiceResult> CreateComments(CommentForUIDto dto);
        Task<ServiceResult> CreateCommentsByMobil(CommentMobilDto dto, string token);
        Task<ServiceResult> Dashboard();
        /// <summary>
        /// onayda beykeleyn bütün yorumları çeker
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult> GetAllCommentsPendingApproval(int readType);


        /// <summary>
        /// Makale ye ait tüm yorumları getitir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<CommentsDto> GetCommentForContent(long id);

        /// <summary>
        /// Makale yorumunu pasif yapar
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ServiceResult> PassiveApproveComment(long Id);

        Task<ServiceResult> ApproveComment(long id);
        Task<ServiceResult> GetAllCommentsPendingApprovalByContentId(long contentId);
        Task<bool> UpsadeContentLog(ContentLogDto dto);
        Task<ServiceResult> ApproveCommentByContent(long id);
    }
}
