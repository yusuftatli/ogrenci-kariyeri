using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common;
using SCA.Common.Base;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class CommentManager : BaseClass, ICommentManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection(ConnectionString1);

        public CommentManager(IErrorManagement errorManagement)
        {
            _errorManagement = errorManagement;
        }

        /// <summary>
        /// makale veya test için yorum insert yapar 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> CreateCommentsByMobil(CommentMobilDto dto, string token)
        {
            ServiceResult res = new ServiceResult();
            long userId = JwtToken.GetUserId(token);
            try
            {
                string query = @"Insert into Comments (ReadType, Description, ArticleId, Approved, UserID, PostDate) values
                               (@ReadType, @Description, @ArticleId, @Approved, @UserID, @PostDate)";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("ReadType", 2);
                filter.Add("Description", dto.Description);
                filter.Add("ArticleId", dto.ArticleId);
                filter.Add("Approved", 0);
                filter.Add("UserID", userId);
                filter.Add("PostDate", DateTime.Now);
                var result = await _db.ExecuteAsync(query,filter);
                res = Result.ReturnAsSuccess(message:"Yorumunuz başarılı bir şekilde keydedilmiştir, onay sürecinden görüntülenecektir.");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, userId, "CreateCommentsByMobil ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail(message:ex.InnerException.ToString());
            }
            return res;
        }

        /// <summary>
        /// makale veya test için yorum insert yapar 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> CreateComments(CommentForUIDto dto)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = $"Insert into Comments (ReadType,Description,ArticleId,Approved,UserID, PostDate) values (2,'{dto.Description}',{dto.ArticleId},0,{dto.UserID}, NOW())";
                var result = await _db.ExecuteAsync(query);
                res = Result.ReturnAsSuccess();
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "CreateComments ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail();
            }
            return res;
        }

        /// <summary>
        /// onayda beykeleyn bütün yorumları çeker
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> GetAllCommentsPendingApprovalByContentId(long contentId)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = $"select * from List_Comments where ArticleId = {contentId} or SycnId = {contentId}";

                var resultData = await _db.QueryAsync<CommentsDto>(query) as List<CommentsDto>;
                res = Result.ReturnAsSuccess(data: resultData);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "CreateComments ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail();
            }
            return res;
        }

        public async Task<bool> UpsadeContentLog(ContentLogDto dto)
        {
            try
            {
                string query = string.Empty;
                query = "update UserContentReadLog set ReadEndData = now() where UserId = @UserId and ReadStartDate = @ReadStartDate;";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("UserId", dto.UserId);
                filter.Add("ReadStartDate", dto.ReadStartDate);

                await _db.ExecuteAsync(query);

            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "UpsadeContentLog ", Entity.Enums.PlatformType.Web);
            }
            return true;
        }

        /// <summary>
        /// onayda beykeleyn bütün yorumları çeker
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult> GetAllCommentsPendingApproval(int readType)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                int _readTypeId = readType == 0 ? 2 : readType;
                string query = $"select c.Id, c.Description as comment, u.Id as UserID,u.Name  as userName," +
                    $" c.PostDate, 'Onayla' buttonName, 'success' as ButtonClass  from Comments c inner join Users u on c.UserID = u.Id " +
                    $"where Approved = 0 and ReadType ={_readTypeId}";

                var resultData = await _db.QueryAsync<CommentsDto>(query) as List<CommentsDto>;
                res = Result.ReturnAsSuccess(data: resultData);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "CreateComments ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail();
            }
            return res;
        }
        public async Task<ServiceResult> Dashboard()
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select count(Id) as  Count from Comments  GROUP BY Approved";

                var result = await _db.QueryAsync<ContentDashboardDto>(query);
                res = Result.ReturnAsSuccess(data: result);
            }
            catch (Exception ex)
            {
                res = res = Result.ReturnAsFail(message: "Kullanıcı dashboard bilgisi yüklenemedi");
                await _errorManagement.SaveError(ex, null, "User/Dashboard", PlatformType.Web);
            }
            return res;
        }

        public async Task<ServiceResult> ApproveComment(long id)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = $"update Comments set Approved =1 where Id={id}";
                await _db.ExecuteAsync(query);
                return Result.ReturnAsSuccess();
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "ApproveComment ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail();
            }
            return res;
        }

        public async Task<ServiceResult> ApproveCommentByContent(long id)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string message = string.Empty;
                var data = await _db.QueryFirstAsync<CommentsDto>($"select  * from Comments where Id = {id}");

                bool updateData = !data.Approved;

                string query = $"update Comments set Approved ={updateData} where Id={id}";
                await _db.ExecuteAsync(query);

                if (updateData)
                {
                    message = "Yorum Onaylandı";
                }
                else
                {
                    message = "Yorum kaldırıldı";
                }

                ServiceResult _serviceResult = await GetAllCommentsPendingApprovalByContentId(data.ArticleId);

                return Result.ReturnAsSuccess(data: _serviceResult.Data, message: message);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "ApproveComment ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail();
            }
            return res;
        }



        /// <summary>
        /// Makale ye ait tüm yorumları getitir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CommentsDto> GetCommentForContent(long id)
        {
            // var data = _mapper.Map<List<CommentsDto>>(_commentRepo.GetAll(x => x.Id == id && x.Approved.Equals(true)));
            return null;
        }

        /// <summary>
        /// Makale yorumunu pasif yapar
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> PassiveApproveComment(long Id)
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                //var data = _commentRepo.Get(x => x.Id == Id);
                //data.Approved = false;
                //_commentRepo.Update(data);
                //_unitOfWork.SaveChanges();
                res = Result.ReturnAsSuccess();
            });
            return res;
        }

    }
}
