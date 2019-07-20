using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface ICommentInterface
    {
        /// <summary>
        /// makale veya test için yorum insert yapar 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ServiceResult> CreateComments(CommentsDto dto);

        /// <summary>
        /// onayda beykeleyn bütün yorumları çeker
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult> GetAllCommentsPendingApproval();

        /// <summary>
        /// makaleye yazılmış olan yorumu admin tarafından onaylar
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ServiceResult> ApproveComment(long Id);

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
    }
}
