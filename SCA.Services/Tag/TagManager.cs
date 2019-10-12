using AutoMapper;
using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.DataAccess.Context;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class TagManager : ITagManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public TagManager( IErrorManagement errorManager)
        {
            _errorManagement = errorManager;
        }

        public async Task<ServiceResult> GetTags()
        {
            ServiceResult res = new ServiceResult();
            try
            {
                var lisData = await _db.QueryAsync<TagDto>("select * from Tags") as List<TagDto>;
                res = Result.ReturnAsSuccess(data: lisData);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetTags", PlatformType.Web);
            }

            return res;
        }

        public bool IsInteger(string value)
        {
            bool result = true;
            try
            {
                Convert.ToInt32(value);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private string GetTagQuery(CrudType crudType, Tags dto, UserSession session)
        {
            string query = "";
            if (crudType == CrudType.Insert)
            {
                query = $"Insert Into Tags (Description, Hit, CreatedUserId,CreatedDate) VALUES " +
                   $"( '{dto.Description}', {dto.Hit}, {session.Id}, '{DateTime.Now}'); SELECT LAST_INSERT_ID();";
            }

            if (crudType == CrudType.Update)
            {
                query = $"Update Tags set Description='{dto.Description}', Hit={dto.Hit},UpdatedUserId={session.Id},UpdatedDate='{DateTime.Now}' where Id={dto.Id}";
            }
            return query;
        }

        private string GetTagRelationQuery(CrudType crudType, TagRelationDto dto, UserSession session)
        {
            string query = "";
            if (crudType == CrudType.Insert)
            {
                query = $" Insert Into TagRelation (TagId,TagContentId,ReadType,CreatedUserId,CreatedDate) VALUES" +
                    $" ({dto.TagId},{dto.TagContentId},'{dto.ReadType}',{session.Id},'{DateTime.Now}') ; SELECT LAST_INSERT_ID();";
            }

            if (crudType == CrudType.Update)
            {

                query = $"Update TagRelation SET TagId={dto.TagId},TagContentId={dto.TagContentId},ReadType= '{dto.ReadType}',UpdatedUserId={session.Id},UpdatedDate={DateTime.Now} where Id={dto.Id}";
            }
            return query;
        }

        public async Task<ServiceResult> CreateTag(string tags, long tagContentId, ReadType ReadType, UserSession session)
        {
            ServiceResult res = new ServiceResult();
            List<TagRelationDto> tagRelationList = new List<TagRelationDto>();
            try
            {
                string query = "";
                string[] _tags = tags.Replace("[", "").Replace("]", "").Replace("\"", "").Replace("\"", "").Split(',');

                foreach (var item in _tags)
                {
                    TagRelation relation = new TagRelation();
                    if (IsInteger(item))
                    {
                        var relationData = new TagRelationDto()
                        {
                            TagId = Convert.ToInt64(item),
                            TagContentId = tagContentId,
                            ReadType = ReadType
                        };
                        query = GetTagRelationQuery(CrudType.Insert, relationData, session);
                        var relationId = _db.Query<long>(query).FirstOrDefault();
                    }
                    else
                    {
                        var data = new Tags()
                        {
                            Description = item,
                            Hit = 1
                        };

                        query = GetTagQuery(CrudType.Insert, data, session);
                        var tagId = _db.Query<long>(query).FirstOrDefault();

                        var relationData = new TagRelationDto()
                        {
                            TagId = tagId,
                            TagContentId = tagContentId,
                            ReadType = ReadType
                        };
                        query = GetTagRelationQuery(CrudType.Insert, relationData, session);
                        var relationId = _db.Execute(query); ;
                    }
                }
                res = Result.ReturnAsSuccess();
            }
            catch (Exception ex)
            {
                res = Result.ReturnAsFail(message: "Makale bağlı taglar kayıt edilirken hata meydana geldi");
                await _errorManagement.SaveError(ex, session.Id, "TagCreate;" + tags, PlatformType.Web);
            }

            return res;
        }

        public async void CreateTagRelation(List<TagRelationDto> dto)
        {
            Task t = new Task(() =>
            {
                //_tagRrlationRepo.AddRange(_mapper.Map<List<TagRelation>>(dto));
            });
            t.Start();
        }

        public async void UpdateTagRelation(List<TagRelationDto> dto)
        {
            //Task t = new Task(() =>
            //{
            //    foreach (var item in dto)
            //    {
            //        var data = _tagRrlationRepo.GetById(item.TagContentId);
            //        _tagRrlationRepo.Delete(data);
            //    }

            //    _tagRrlationRepo.AddRange(_mapper.Map<List<TagRelation>>(dto));
            //    _unitOfWork.SaveChanges();
            //});
            //t.Start();
        }

        public Task<ServiceResult> CreateTag(string tags)
        {
            throw new NotImplementedException();
        }
    }
}
