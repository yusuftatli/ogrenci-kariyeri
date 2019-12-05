using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class SocialMediaManager : ISocialMediaManager
    {

        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public SocialMediaManager(IErrorManagement em)
        {
            _errorManagement = em;
        }


        public async Task<ServiceResult> CreateSocialMedia(UserWeblDto dto, long userId)
        {
            ServiceResult res = new ServiceResult();

            DynamicParameters filter = new DynamicParameters();
            try
            {
                List<SocialMediaDto> listSocial = new List<SocialMediaDto>();

                listSocial.Add(new SocialMediaDto()
                {
                    SocialMediaType = SocialMediaType.Facebook,
                    UserId = dto.Id,
                    IsActive = true,
                    Url = dto.Facebook
                });
                listSocial.Add(new SocialMediaDto()
                {
                    SocialMediaType = SocialMediaType.Instagram,
                    UserId = dto.Id,
                    IsActive = true,
                    Url = dto.Instagram
                });
                listSocial.Add(new SocialMediaDto()
                {
                    SocialMediaType = SocialMediaType.Linkedin,
                    UserId = dto.Id,
                    IsActive = true,
                    Url = dto.Linkedin
                });

                listSocial.Add(new SocialMediaDto()
                {
                    SocialMediaType = SocialMediaType.Linkedin,
                    UserId = dto.Id,
                    IsActive = true,
                    Url = dto.Youtube
                });

                listSocial.Add(new SocialMediaDto()
                {
                    SocialMediaType = SocialMediaType.Linkedin,
                    UserId = dto.Id,
                    IsActive = true,
                    Url = dto.Twitter
                });

                listSocial.Add(new SocialMediaDto()
                {
                    SocialMediaType = SocialMediaType.Linkedin,
                    UserId = dto.Id,
                    IsActive = true,
                    Url = dto.GooglePlus
                });

                string query = string.Empty;
                string deleteQuery = string.Empty;
                string em = string.Empty;
                foreach (SocialMediaDto item in listSocial)
                {
                    if (item.Id == 0)
                    {
                        query = @"insert into SocialMedia (UserId,CompanyClupId,Url,IsActive,SocialMediaType,CreatedUserId,CreatedDate)
                                                 values (@UserId,@CompanyClupId,@Url,@IsActive,@SocialMediaType,@CreatedUserId,@CreatedDate);";

                        filter.Add("UserId", item.UserId);
                        filter.Add("CompanyClupId", item.CompanyClupId);
                        filter.Add("Url", item.Url);
                        filter.Add("IsActive", item.IsActive);
                        filter.Add("SocialMediaType", item.SocialMediaType);
                        filter.Add("CreatedUserId", userId);
                        filter.Add("CreatedDate", DateTime.Now);
                        var result = _db.Execute(query, filter);
                        em = "Sosyal medya url kayıt işlemi başarılı";
                    }
                    else
                    {
                        deleteQuery = "delete from SocialMedia where Id=@Id";
                        filter.Add("Id", item.UserId);

                        var delete = _db.Execute(query, filter);

                        query = "update SocialMedia set UserId=@UserId,CompanyClupId=@CompanyClupId,Url=@Url,IsActive=@IsActive,SocialMediaType=@SocialMediaType,UpdatedUserId=@UpdatedUserId,UpdatedDate=@UpdatedDate where Id=@Id";
                        filter.Add("Id", item.Id);
                        filter.Add("UserId", item.UserId);
                        filter.Add("CompanyClupId", item.CompanyClupId);
                        filter.Add("Url", item.Url);
                        filter.Add("IsActive", item.IsActive);
                        filter.Add("SocialMediaType", item.SocialMediaType);
                        filter.Add("UpdatedUserId", userId);
                        filter.Add("UpdatedDate", DateTime.Now);
                        var addSocialData = _db.Execute(query, filter);
                        em = "Sosyal meyda url güncelleme işlemi başarlı";

                        res = Result.ReturnAsSuccess(message: em);
                    }
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "CreateSocialMedia", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Sosyal medya url kayıt işlemi sırasında hata meydana geldi.");
            }
            return res;
        }

        public async Task<List<SocialMediaDto>> GetSocialMedia(long id)
        {
            List<SocialMediaDto> res = new List<SocialMediaDto>();
            try
            {
                string query = @"select * from SocialMedia where UserId = @Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", id);

                var resultData = await _db.QueryAsync<SocialMediaDto>(query, filter) as List<SocialMediaDto>;
                res = resultData;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetSocialMedia", PlatformType.Mobil);
            }
            return res;
        }
    }
}
