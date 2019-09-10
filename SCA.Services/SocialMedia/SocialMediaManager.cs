using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class SocialMediaManager : ISocialMediaManager
    {

        private readonly IErrorManagement _em;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public SocialMediaManager(IErrorManagement em)
        {
            _em = em;
        }


        public async Task<ServiceResult> CreateSocialMedia(SocialMediaDto dto, long userId)
        {
            ServiceResult _res =new ServiceResult();
           
            DynamicParameters filter = new DynamicParameters();
            try
            {
                string query = "";
                string deleteQuery = "";
                string em = "";
                if (dto.Id == 0)
                {
                    query = @"insert into SocialMedia (UserId,CompanyClupId,Url,IsActive,SocialMediaType,CreatedUserId,CreatedDate)
                                                 values (@UserId,@CompanyClupId,@Url,@IsActive,@SocialMediaType,@CreatedUserId,@CreatedDate);";

                    filter.Add("UserId", userId);
                    filter.Add("CompanyClupId", dto.CompanyClupId);
                    filter.Add("Url", dto.Url);
                    filter.Add("IsActive", dto.IsActive);
                    filter.Add("SocialMediaType", dto.SocialMediaType);
                    filter.Add("CreatedUserId", userId);
                    filter.Add("CreatedDate", DateTime.Now);
                    var res = _db.Execute(query, filter);
                    em = "Sosyal medya url kayıt işlemi başarılı";
                }
                else
                {
                    deleteQuery = "delete from SocialMedia where Id=@Id";
                    filter.Add("Id", userId);

                    var delete = _db.Execute(query, filter);

                    query = "update SocialMedia set UserId=@UserId,CompanyClupId=@CompanyClupId,Url=@Url,IsActive=@IsActive,SocialMediaType=@SocialMediaType,UpdatedUserId=@UpdatedUserId,UpdatedDate=@UpdatedDate where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("UserId", userId);
                    filter.Add("CompanyClupId", dto.CompanyClupId);
                    filter.Add("Url", dto.Url);
                    filter.Add("IsActive", dto.IsActive);
                    filter.Add("SocialMediaType", dto.SocialMediaType);
                    filter.Add("UpdatedUserId", userId);
                    filter.Add("UpdatedDate", DateTime.Now);
                    var addSocialData = _db.Execute(query, filter);
                    em = "Sosyal meyda url güncelleme işlemi başarlı";

                    _res = Result.ReturnAsSuccess(message:em);
                }
            }
            catch (Exception ex)
            {
               await _em.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message:"Sosyal medya url kayıt işlemi sırasında hata meydana geldi.");
            }
            return _res;
        }
    }
}
