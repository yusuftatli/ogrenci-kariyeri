using AutoMapper;
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
        public TagManager(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tagRepo = unitOfWork.GetRepository<Tags>();
            _tagRrlationRepo = unitOfWork.GetRepository<TagRelation>();
        }

        public async Task<ServiceResult> GetTags()
        {
            ServiceResult _res = new ServiceResult();
            Task t = new Task(() =>
            {
                _res = Result.ReturnAsSuccess(null, _mapper.Map<List<TagDto>>(_tagRepo.GetAll()));
            });
            t.Start();
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

        public async Task<ServiceResult> CreateTag(string tags, long tagContentId, ReadType ReadType)
        {
            ServiceResult _res = new ServiceResult();
            Task t = new Task(() =>
            {
                List<long> resultdata = new List<long>();
                List<TagRelationDto> tagRelationList = new List<TagRelationDto>();

                string[] _tags = tags.Replace("[", "").Replace("]", "").Replace("\"", "").Replace("\"", "").Split(',');

                PostgreDbContext db = new PostgreDbContext();

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
                        tagRelationList.Add(relationData);
                    }
                    else
                    {
                        var data = new Tags()
                        {
                            Id = 0,
                            Description = item,
                            Hit = 1
                        };

                        db.Tags.Add(data);
                        db.SaveChanges();

                        var relationData = new TagRelationDto()
                        {
                            TagId = data.Id,
                            TagContentId = tagContentId,
                            ReadType = ReadType
                        };
                        tagRelationList.Add(relationData);
                    }
                    CreateTagRelation(tagRelationList);
                }
                _res = Result.ReturnAsSuccess("", _mapper.Map<List<TagDto>>(_tagRepo.GetAll()));
            });
            t.Start();
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
