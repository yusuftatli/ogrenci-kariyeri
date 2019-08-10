using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IContentManager
    {
        /// <summary>
        /// makale kısa açıklamalarını listeler
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult> ContentShortList(ContentSearchDto dto);

        /// <summary>
        /// Makaleleri içerikleri ile birlikte listeler
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult> ContentList();

        /// <summary>
        /// makale ekler
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ServiceResult> ContentCreate(ContentDto dto);

        /// <summary>
        /// makale siler
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ServiceResult> ContentDelete(long Id);

        /// <summary>
        /// makalelerin yayınlanma durumunu günceller
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="publishState"></param>
        /// <returns></returns>
        Task<ServiceResult> UpdateAssayState(long Id, PublishState publishState);

        Task<ServiceResult> UpdateContentPublish(long id, PublishState publishState);
        Task<ServiceResult> ContentShortListForUI();

        /// <summary>
        /// makaleleri short list döner 
        /// </summary>
        /// <param name="hitTypes"></param>
        /// <param name="count"></param>
        /// <returns>List<ContentForHomePageDTO></returns>
        Task<List<ContentForHomePageDTO>> GetContentForHomePage(HitTypes hitTypes, int count);
    }
}
