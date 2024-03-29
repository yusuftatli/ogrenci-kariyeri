﻿using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SCA.Common.Base;
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
    public class TagManager : BaseClass, ITagManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection(ConnectionString1);

        public TagManager(IErrorManagement errorManager)
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

        public async Task<List<TagDto>> GetTagsForUI()
        {
            List<TagDto> res = new List<TagDto>();
            try
            {
                res = await _db.QueryAsync<TagDto>("TagListAll", commandType: CommandType.StoredProcedure) as List<TagDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "TagListAll", PlatformType.Web);
            }

            return res;
        }
        public async Task<List<TagDto>> GetTagListById(long contentId)
        {
            List<TagDto> res = new List<TagDto>();
            try
            {
                DynamicParameters filter = new DynamicParameters();
                filter.Add("id", contentId);
                var lisData = await _db.QueryAsync<TagDto>("select t.Id, t.Description from TagRelation r inner join Tags t on r.TagId = t.Id where r.TagContentId =@Id  ", filter) as List<TagDto>;
                res = lisData;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, contentId, "GetTagById", PlatformType.Web);
            }

            return res;
        }

        public async Task<string> GetTagById(long contentId)
        {
            string res = string.Empty;
            try
            {
                var lisData = await _db.QueryAsync<TagDto>(" select t.Description from TagRelation r inner join Tags t on r.TagId = t.Id where r.TagContentId = 5929") as List<TagDto>;
                foreach (TagDto item in lisData)
                {
                    res += " " + item.Description;
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, contentId, "GetTagById", PlatformType.Web);
            }

            return res;
        }

        public async Task<List<long>> GetTagIdByContentId(long contentId)
        {
            List<long> _res = new List<long>();
            try
            {
                string query = $"select t.Id from TagRelation tr " +
                    $"left join Tags t on t.Id = tr.TagId where TagContentId = { contentId }";
                var lisData = await _db.QueryAsync<long>(query) as List<long>;
                _res = lisData;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, contentId, "GetTagById", PlatformType.Web);
            }

            return _res;
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
                query = $"delete from CategoryRelation where TagContentId={dto.TagContentId};" +
                    $" Insert Into TagRelation (TagId,TagContentId,ReadType,CreatedUserId,CreatedDate) VALUES" +
                    $" ({dto.TagId},{dto.TagContentId},'{dto.ReadType}',{session.Id},'{DateTime.Now}') ; SELECT LAST_INSERT_ID();";
            }

            if (crudType == CrudType.Update)
            {

                query = $"Update TagRelation SET TagId={dto.TagId},TagContentId={dto.TagContentId},ReadType= '{dto.ReadType}',UpdatedUserId={session.Id},UpdatedDate={DateTime.Now} where Id={dto.Id}";
            }
            return query;
        }

        public async Task<string> CreateTag(string tags, long tagContentId, ReadType ReadType, UserSession session)
        {
            string res = string.Empty;
            List<long> idList = new List<long>();
            try
            {

                var tagData = await _db.QueryFirstOrDefaultAsync<TagRelationDto>($"select * from TagRelation where TagContentId = {tagContentId}");

                if (tagData != null)
                {
                    await _db.ExecuteAsync($"delete from CategoryRelation where TagContentId ={ tagContentId};");
                }

                string query = "";
                string[] _tags = tags.Replace("[", "").Replace("]", "").Replace("\"", "").Replace("\"", "").Split(',');

                foreach (var item in _tags)
                {
                    TagRelation relation = new TagRelation();
                    if (IsInteger(item))
                    {
                        query = string.Empty;
                        query = $"Insert Into TagRelation (TagId, TagContentId) VALUES ({Convert.ToInt64(item)},{tagContentId})";
                        var relationId = await _db.QueryFirstOrDefaultAsync<TagRelationDto>(query);
                    }
                    else
                    {
                      

                        query = string.Empty;
                        query = $"Insert Into Tags (Description) values('{item}')";
                        await _db.ExecuteAsync(query);
                        query = $"select * from Tags where Description= '{item}'";
                        var tagD = await _db.QueryFirstAsync<TagDto>(query);
                        query = $"Insert Into TagRelation (TagId, TagContentId) VALUES ({Convert.ToInt64(tagD.Id)},{tagContentId})";
                        var relationId = await _db.QueryFirstOrDefaultAsync<TagRelationDto>(query);
                    }
                 
                }
                foreach (long item in idList)
                {
                    res += item.ToString();
                }
            }
            catch (Exception ex)
            {
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
