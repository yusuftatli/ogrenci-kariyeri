using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface ICompanyClubManager
    {
        Task<bool> FollowCompany(long userId, string seoUrl, string follow);
        Task<ServiceResult> GetAllCompaniesClubs(CompanyClupType companyClupType);

        Task<ServiceResult> GetCompanyId(string seourl);

        Task<ServiceResult> CreateCompanyClubs(CompanyClubsDto dto, UserSession session);

        Task<ServiceResult> GetCompanyHeader(string seoUrl, long userId);

        Task<ServiceResult> GetCompanyInformation(string seoUrl);

        Task<ServiceResult> GetCompanyAnnouncements(string seoUrl);

        Task<ServiceResult> AddOrUpdateAnnouncement(AnouncementDto model);

        Task<ServiceResult> DeleteCompanyAnnouncement(long id);

        Task<ServiceResult> GetCompanyYoutubePlayList(string seoUrl);

        Task<ServiceResult> AddOrUpdateYoutubePlaylist(YoutubeVideo model);

        Task<ServiceResult> DeleteYoutubePlaylistItem(long id);
    }
}
