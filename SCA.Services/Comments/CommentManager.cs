using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class CommentManager : ICommentManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

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
                string query = $"Insert into Comments (ReadType,Description,ArticleId,Approv,d,UserID, PostDate) values (2,'{dto.Description}',{dto.ArticleId},0,{userId}, NOW())";
                var result = await _db.ExecuteAsync(query);
                res = Result.ReturnAsSuccess();
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, userId, "CreateCommentsByMobil ", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail();
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
        public async Task<ServiceResult> GetAllCommentsPendingApproval()
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                //var result = _mapper.Map<List<CommentsDto>>((_commentRepo.GetAll(x => x.Approved.Equals(false))));
                res = Result.ReturnAsSuccess();
            });
            return res;
        }

        /// <summary>
        /// makaleye yazılmış olan yorumu admin tarafından onaylar
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> ApproveComment(long Id)
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                //var data = _commentRepo.Get(x => x.Id == Id);
                //data.Approved = true;
                //_commentRepo.Update(data);
                //_unitOfWork.SaveChanges();
                res = Result.ReturnAsSuccess();
            });
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
