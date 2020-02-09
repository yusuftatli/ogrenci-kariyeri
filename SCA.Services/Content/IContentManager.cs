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
        Task<ServiceResult> ContentShortList(ContentSearchDto dto, UserSession session);

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
        Task<ServiceResult> ContentCreate(ContentDto dto, UserSession session);


        /// <summary>
        /// makalelerin yayınlanma durumunu günceller
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="publishState"></param>
        /// <returns></returns>
        Task<ServiceResult> UpdateAssayState(long Id, PublishState publishState);

        Task<ServiceResult> UpdateContentPublish(publishStateDto dto, UserSession session);
        Task<ServiceResult> ContentShortListForUI();

        /// <summary>
        /// makaleleri short list döner 
        /// </summary>
        /// <param name="hitTypes"></param>
        /// <param name="count"></param>
        /// <returns>List<ContentForHomePageDTO></returns>
        Task<List<ContentForHomePageDTO>> GetContentForHomePage(HitTypes hitTypes, int count, int offset = 0);

        Task<ServiceResult> GetContent(long id);

        /// <summary>
        /// makale detay servisi
        /// </summary>
        /// <param name="seoUrl"></param>
        /// <returns></returns>
        Task<ContentDetailForDetailPageDTO> GetContentUI(string seoUrl, long? userId = null, string ip = "");
        /// <summary>
        /// mobil makale detay servisi
        /// </summary>
        /// <param name="seoUrl"></param>
        /// <returns></returns>
        Task<ServiceResult> GetContentForMobil(string seoUrl);

        Task<ServiceResult> CreateContentSyncData(ContentDto dto);

        Task<List<FavoriteDto>> GetFavoriteContents(int count);

        Task<List<ContentForHomePageDTO>> GetUsersFavoriteContents(long userId, int count);

        Task<bool> CreateFavorite(FavoriteDto dto);
        Task<ServiceResult> ContentShortListByMobil(ContentSearchByMoilDto dto, string token);
        Task<ServiceResult> GetContentByMobil(ContentDetailMobilDto dto, string token);
        Task<ServiceResult> GetFavoriteContents(int count, string token);
        Task<ServiceResult> CreateFavorite(FavoriteMobilDto dto, string token);
        Task<ServiceResult> ContentShortListFavoriByMobil(int count, string token);
        Task<ServiceResult> GetSearch(string value);
        Task<List<TagDto>> GetAllTagsUI();
        Task<ServiceResult> GetAllTags();
        Task<ServiceResult> Dashboard();
        Task<ServiceResult> UpdateMenuSide(long contentId, int state);
        Task<ServiceResult> GetMenuSideState(long contentId);
        Task<List<ContentForHomePageDTO>> GetContentForTopAndBottomSlide();

        /// <summary>
        /// haberlerin platform türü ayarı yapılır.
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<ServiceResult> UpdatePlatformType(long contentId, int type);
    }
}
