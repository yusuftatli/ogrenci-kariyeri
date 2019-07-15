using AutoMapper;
using SCA.Common.Result;
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
    public class AnalysisManager : IAnalysisManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<UserCreateAnlitic> _userCreateAnliticRepo;
        private IGenericRepository<ReadCountOfTestAndContent> _readContentRepo;

        public AnalysisManager(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _readContentRepo = unitOfWork.GetRepository<ReadCountOfTestAndContent>();
            _userCreateAnliticRepo = unitOfWork.GetRepository<UserCreateAnlitic>();
        }

        public async Task<ServiceResult> GetUserCreateAnlitic()
        {
            var data = _mapper.Map<List<UserCreateAnliticDto>>(_userCreateAnliticRepo.GetAll());
            return Result.ReturnAsSuccess(null, data);
        }

        public async Task<ServiceResult> LogUserCreateanalitic(PlatformType platformType)
        {
            var data = _userCreateAnliticRepo.Get(x => x.MethodTypeId == MethodType.LoginCreate);
            if (data.Equals(null))
            {
                UserCreateAnlitic dto = new UserCreateAnlitic();
                dto.MethodTypeId = MethodType.LoginCreate;
                dto.WebCount = 1;
                if (platformType == PlatformType.Mobil)
                {
                    dto.MobilCount = 1;
                }
                else
                {
                    dto.WebCount = 1;
                }
                _userCreateAnliticRepo.Add(_mapper.Map<UserCreateAnlitic>(dto));
            }
            else
            {
                if (platformType == PlatformType.Mobil)
                {
                    data.MobilCount = data.MobilCount + 1;
                }
                else
                {
                    data.WebCount = data.WebCount + 1;
                }
                _userCreateAnliticRepo.Update(_mapper.Map<UserCreateAnlitic>(data));
            }

            return _unitOfWork.SaveChanges();
        }

        public async Task<ServiceResult> LogReadTestOrContent(ReadType readType, PlatformType platformType, long id)
        {
            if (readType == ReadType.Test)
            {
                var data = _readContentRepo.Get(x => x.ReadId == id);
                if (data != null)
                {
                    data.Count = data.Count + 1;
                }
                else
                {
                    ReadCountOfTestAndContentDto dto = new ReadCountOfTestAndContentDto();
                    dto.Count = 1;
                    dto.ReadId = id;
                    dto.ReadType = ReadType.Test;
                    dto.UserId = 1;
                    dto.PlatformType = platformType;
                    dto.InsertDate = DateTime.Now;

                    _readContentRepo.Add(_mapper.Map<ReadCountOfTestAndContent>(dto));
                }
            }

            if (readType == ReadType.Content)
            {
                var data = _readContentRepo.Get(x => x.ReadId == id);
                if (data != null)
                {
                    data.Count = data.Count + 1;
                }
                else
                {
                    ReadCountOfTestAndContentDto dto = new ReadCountOfTestAndContentDto();
                    dto.Count = 1;
                    dto.ReadId = id;
                    dto.ReadType = ReadType.Content;
                    dto.UserId = 1;
                    dto.PlatformType = platformType;
                    dto.InsertDate = DateTime.Now;

                    _readContentRepo.Add(_mapper.Map<ReadCountOfTestAndContent>(dto));
                }
            }

            var res = _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(null, null);
        }

        public  long GetCountValue(ReadType readType, long id)
        {
            return  _readContentRepo.Get(x => x.ReadId == id && x.ReadType == readType).Count;
        }
    }
}
