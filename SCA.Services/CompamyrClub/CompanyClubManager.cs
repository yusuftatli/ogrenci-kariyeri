using Dapper;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class CompanyClubManager : ICompanyClubManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly ISocialMediaManager _socialmanager;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public CompanyClubManager(IErrorManagement errorManagement, ISocialMediaManager socialManager)
        {
            _errorManagement = errorManagement;
            _socialmanager = socialManager;
        }

        public async Task<ServiceResult> GetAllCompaniesClubs(CompanyClupType companyClupType)
        {
            ServiceResult res = new ServiceResult();
            string flag = (companyClupType == CompanyClupType.Club) ? "Şirket" : "Klüp";
            try
            {
                string query = "select * from CompanyClubs where CompanyClupType=@CompanyClupType AND DeletedDate is null";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("CompanyClupType", companyClupType);
                var resultData = _db.Query<CompanyClubsDto>(query, filter).ToList();

                if (resultData.Count > 0)
                {
                    res = Result.ReturnAsSuccess(data: resultData);
                }
                else
                {
                    res = Result.ReturnAsFail(message: $"{flag} bilgisi yüklenirken hata meydana geldi");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryListWithParents ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail(message: $"{flag} bilgisi yüklenirken hata meydana geldi");
            }
            return res;
        }

        public async Task<ServiceResult> GetCompanyId(string seoUrl)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select * from CompanyClubs where SeoUrl=@SeoUrl";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("SeoUrl", seoUrl);

                var result = _db.Query<CompanyClubsDto>(query, filter).FirstOrDefault();
                if (result != null)
                {
                    res = Result.ReturnAsSuccess(data: result);
                }
                else
                {
                    res = Result.ReturnAsFail(message: "Aranılan şirket bulunamadı");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryListWithParents ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail(message: "Şirket bilgisi yüklenirken hata meydana geldi.");
            }

            // var data = _companyClubsRepo.Get(x => x.Id == 0);
            return Result.ReturnAsSuccess(null, message: AlertResource.SuccessfulOperation, data: null);
        }

        public async Task<ServiceResult> CreateCompanyClubs(CompanyClubsDto dto, UserSession session)
        {

            ServiceResult res = new ServiceResult();
            DynamicParameters filter = new DynamicParameters();
            string query = "";
            if (dto.Equals(null))
            {
                return Result.ReturnAsFail();
            }

            try
            {
                if (dto.Id == 0)
                {
                    query = $"insert into CompanyClubs (CompanyClupType, ShortName, SectorType, SeoUrl, HeaderImage, SectorId, UserId, CreateUserName, Description, WebSite, PhoneNumber,EmailAddress,CreatedUserId,CreatedDate) values " +
                    $"                              (@CompanyClupType, @ShortName, @SectorType, @SeoUrl, @HeaderImage, @SectorId, @UserId, @CreateUserName, @Description, @WebSite, @PhoneNumber,@EmailAddress,@CreatedUserId,@CreatedDate); SELECT LAST_INSERT_ID();";
                    filter.Add("CompanyClupType", dto.CompanyClupType);
                    filter.Add("ShortName", dto.ShortName);
                    filter.Add("ImageDirectory", dto.ImageDirectory);
                    filter.Add("SectorType", dto.SectorType);
                    filter.Add("SeoUrl", dto.SeoUrl);
                    filter.Add("SectorId", dto.SectorId);
                    filter.Add("HeaderImage", dto.HeaderImage);
                    filter.Add("UserId", session.Id);
                    filter.Add("CreateUserName", session.Name);
                    filter.Add("Description", dto.Description);
                    filter.Add("WebSite", dto.WebSite);
                    filter.Add("PhoneNumber", dto.PhoneNumber);
                    filter.Add("EmailAddress", dto.EmailAddress);
                    filter.Add("CreatedUserId", session.Id);
                    filter.Add("CreatedDate", DateTime.Now);
                }
                else
                {
                    query = $"Update CompanyClubs set  CompanyClupType=@CompanyClupType, ImageDirectory=@ImageDirectory, ShortName=@ShortName, SectorType=@SectorType, SeoUrl=@SeoUrl, HeaderImage=@HeaderImage, SectorId=@SectorId, UserId=@UserId, UpdatedUserId=@UpdatedUserId, UpdatedDate=@UpdatedDate, " +
                        $"Description=@Description, WebSite=@WebSite, PhoneNumber=@PhoneNumber,EmailAddress=@EmailAddress where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("CompanyClupType", dto.CompanyClupType);
                    filter.Add("ShortName", dto.ShortName);
                    filter.Add("ImageDirectory", dto.ImageDirectory);
                    filter.Add("SectorType", dto.SectorType);
                    filter.Add("SeoUrl", dto.SeoUrl);
                    filter.Add("SectorId", dto.SectorId);
                    filter.Add("UserId", session.Id);
                    filter.Add("CreateUserName", session.Name);
                    filter.Add("Description", dto.Description);
                    filter.Add("WebSite", dto.WebSite);
                    filter.Add("PhoneNumber", dto.PhoneNumber);
                    filter.Add("EmailAddress", dto.EmailAddress);
                    filter.Add("UpdatedUserId", session.Id);
                    filter.Add("UpdatedDate", DateTime.Now);
                }

                var result = _db.Execute(query, filter);

                List<SocialMediaDto> socialData = new List<SocialMediaDto>();
                socialData.Add(new SocialMediaDto { CompanyClupId = result, IsActive = true, SocialMediaType = SocialMediaType.Facebook, Url = dto.Facebook, UserId = null });
                socialData.Add(new SocialMediaDto { CompanyClupId = result, IsActive = true, SocialMediaType = SocialMediaType.Linkedin, Url = dto.Linkedin, UserId = null });
                socialData.Add(new SocialMediaDto { CompanyClupId = result, IsActive = true, SocialMediaType = SocialMediaType.Instagram, Url = dto.Instagram, UserId = null });


                //foreach (var item in socialData)
                //{
                //    await _socialmanager.CreateSocialMedia(item, session.Id);
                //}
                string flag = (dto.CompanyClupType == CompanyClupType.Club) ? "Şirket" : "Klüp";
                res = Result.ReturnAsSuccess(message: flag + " Başarıyla kaydedildi");
            }
            catch (Exception ex)
            {

                await _errorManagement.SaveError(ex, 0, "MainCategoryListWithParents ", Entity.Enums.PlatformType.Web);
            }


            dto.UserId = session.Id;
            CompanyClubs resData = new CompanyClubs();

            //if (dto.Id == 0)
            //{
            //    resData = _companyClubsRepo.Add(_mapper.Map<CompanyClubs>(dto));
            //    resultMessage = "Kayıt İşlemi Başarılı";
            //}
            //else
            //{
            //    _companyClubsRepo.Update(_mapper.Map<CompanyClubs>(dto));
            //    resultMessage = "Güncelleme İşlemi Başarılı";
            //    var updataSocialData = _socialMediaRepo.GetAll(x => x.CompanyClupId == dto.Id);
            //    foreach (var item in updataSocialData)
            //    {
            //        _socialMediaRepo.Delete(item);
            //    }
            //}
            return res;
        }

        public async Task<bool> FollowCompany(long userId, string seoUrl, string follow)
        {
            try
            {
                string query = "delete from CompanyFollows where UserId = userId and CompanyId = (select Id from CompanyClubs where SeoUrl = @seoUrl);" +
                                    "insert into CompanyFollows(UserId, CompanyId, Follow) values(@userId, (select Id from CompanyClubs where SeoUrl = @seoUrl), @follow); ";

                DynamicParameters filter = new DynamicParameters();
                filter.Add("seoUrl", seoUrl);
                filter.Add("userId", userId);
                filter.Add("follow", follow);
               await _db.ExecuteAsync(query, filter);

            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, userId, "GetContentUI", PlatformType.Mobil);
            }
            return true;
        }

        public async Task<ServiceResult> GetCompanyHeader(string seoUrl, long userId)
        {
            try
            {
                using (var multi = await _db.QueryMultipleAsync("GetCompanyHeader", new { seoUrl = seoUrl }, commandType: CommandType.StoredProcedure))
                {
                    var companyHeader = await multi.ReadFirstOrDefaultAsync<CompClubHeaderDto>();
                    companyHeader.SocialMedias = await multi.ReadAsync<SocialMediaDto>() as List<SocialMediaDto>;
                    return Result.ReturnAsSuccess(data: companyHeader);
                }

                bool result = await CompanyShownLog(seoUrl, userId);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryListWithParents ", Entity.Enums.PlatformType.Web);
                return Result.ReturnAsFail(message: "Hata");
            }
        }

        public async Task<bool> CompanyShownLog(string seourl, long userId)
        {
            try
            {
                string query = "insert into CompanyUserLog(CompanyId, UserId) values((select Id from CompanyClubs where SeoUrl = @SeoUrl), @UserId);";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("SeoUrl", seourl);
                filter.Add("UserId", userId);
                var result = _db.ExecuteAsync(query, filter);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryCreate ", Entity.Enums.PlatformType.Web);
            }
            return true;
        }

        public async Task<ServiceResult> GetCompanyInformation(string seoUrl)
        {
            try
            {
                var res = await _db.QueryFirstOrDefaultAsync<CompanyClubInformationDto>("GetCompanyInformation", new { seoUrl }, commandType: CommandType.StoredProcedure);
                return Result.ReturnAsSuccess(data: res);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "MainCategoryListWithParents ", Entity.Enums.PlatformType.Web);
                return Result.ReturnAsFail(message: "Hata");
            }
        }
        #region Announcement
        public async Task<ServiceResult> GetCompanyAnnouncements(string seoUrl)
        {
            var res = await _db.QueryAsync<AnouncementDto>("GetCompanyAnnouncements", new { seoUrl }, commandType: CommandType.StoredProcedure) as List<AnouncementDto>;
            return Result.ReturnAsSuccess(data: res);
        }

        public async Task<ServiceResult> AddOrUpdateAnnouncement(AnouncementDto model)
        {
            var res = await _db.ExecuteScalarAsync<long>("AddOrUpdateCompanyAnnouncement", model, commandType: CommandType.StoredProcedure);
            return Result.ReturnAsSuccess(data: res);
        }
        public async Task<ServiceResult> DeleteCompanyAnnouncement(long id)
        {
            await _db.ExecuteAsync("DeleteCompanyAnnouncement", new { AnnouncementId = id }, commandType: CommandType.StoredProcedure);
            return Result.ReturnAsSuccess();
        }
        #endregion

        #region Youtube Playlist
        public async Task<ServiceResult> GetCompanyYoutubePlayList(string seoUrl)
        {
            var res = await _db.QueryAsync<YoutubeVideo>("GetCompanyYoutubePlaylist", new { seoUrl = seoUrl }, commandType: CommandType.StoredProcedure) as List<YoutubeVideo>;
            return Result.ReturnAsSuccess(data: res);
        }

        public async Task<ServiceResult> AddOrUpdateYoutubePlaylist(YoutubeVideo model)
        {
            var res = await _db.ExecuteScalarAsync<long>("AddOrUpdateYoutubePlaylist", model, commandType: CommandType.StoredProcedure);
            return Result.ReturnAsSuccess(data: res);
        }

        public async Task<ServiceResult> DeleteYoutubePlaylistItem(long id)
        {
            await _db.ExecuteAsync("DeleteYoutubePlaylistItem", new { ItemId = id }, commandType: CommandType.StoredProcedure);
            return Result.ReturnAsSuccess();
        }
        #endregion


    }
}
