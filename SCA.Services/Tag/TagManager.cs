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
using SCA.Repository.Repo;
using SCA.Repository.UoW;
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
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Tags> _tagRepo;
        private IGenericRepository<TagRelation> _tagRrlationRepo;
        private readonly IErrorManagement _errorManager;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public TagManager(IUnitofWork unitOfWork, IMapper mapper, IErrorManagement errorManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tagRepo = unitOfWork.GetRepository<Tags>();
            _tagRrlationRepo = unitOfWork.GetRepository<TagRelation>();
            _errorManager = errorManager;
        }

        public async Task<ServiceResult> GetTags()
        {
            ServiceResult _res = new ServiceResult();
            List<TagDto> listData = new List<TagDto>();
            try
            {
                Task t = new Task(() =>
                            {
                                listData = _db.Query<TagDto>("select * from Tags").ToList();
                                _res = Result.ReturnAsSuccess(data: listData);
                            });
                t.Start();
            }
            catch (Exception ex)
            {
                await _errorManager.SaveError(ex.ToString());
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
                query = $"Insert Into Tags (Description, Hit, CreatedUserId,CreatedDate,DeletedDate) VALUES " +
                   $"( {dto.Description}, {dto.Hit}, {session.Id}, {DateTime.Now},  {false}); SELECT LAST_INSERT_ID();";
            }

            if (crudType == CrudType.Update)
            {
                query = $"Update Tags set Description={dto.Description}, Hit={dto.Hit},UpdatedUserId={session.Id},UpdatedDate={DateTime.Now} where Id={dto.Id}";
            }
            return query;
        }

        private string GetTagRelationQuery(CrudType crudType, TagRelationDto dto, UserSession session)
        {
            string query = "";
            if (crudType == CrudType.Insert)
            {
                query = $" Insert Into TagRelation (TagId,TagContentId,ReadType,CreatedUserId,CreatedDate) VALUES" +
                    $" ({dto.TagId},{dto.TagContentId},{dto.ReadType},{session.Id},{DateTime.Now}) ; SELECT LAST_INSERT_ID();";
            }

            if (crudType == CrudType.Update)
            {

                query = $"Update TagRelation SET TagId={dto.TagId},TagContentId={dto.TagContentId},ReadType={dto.ReadType},UpdatedUserId={session.Id},UpdatedDate={DateTime.Now} where Id={dto.Id}";
            }
            return query;
        }

        public async Task<ServiceResult> CreateTag(string tags, long tagContentId, ReadType ReadType, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            List<TagRelationDto> tagRelationList = new List<TagRelationDto>();
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
                    var relationId = _db.Execute(query);
                }
                else
                {
                    var data = new Tags()
                    {
                        Id = 0,
                        Description = item,
                        Hit = 1
                    };

                    query = GetTagQuery(CrudType.Insert, data, session);
                    var tagId = _db.Execute(query);

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
            return _res;
        }

        public async void CreateTagRelation(List<TagRelationDto> dto)
        {
            Task t = new Task(() =>
            {
                _tagRrlationRepo.AddRange(_mapper.Map<List<TagRelation>>(dto));
            });
            t.Start();
        }

        public async void UpdateTagRelation(List<TagRelationDto> dto)
        {
            Task t = new Task(() =>
            {
                foreach (var item in dto)
                {
                    var data = _tagRrlationRepo.GetById(item.TagContentId);
                    _tagRrlationRepo.Delete(data);
                }

                _tagRrlationRepo.AddRange(_mapper.Map<List<TagRelation>>(dto));
                _unitOfWork.SaveChanges();
            });
            t.Start();
        }

        public Task<ServiceResult> CreateTag(string tags)
        {
            throw new NotImplementedException();
        }
    }
}
