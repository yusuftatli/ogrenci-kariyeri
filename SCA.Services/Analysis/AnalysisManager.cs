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
        private IGenericRepository<ReadCountOfTestAndContent> _readContentRepo;

        public AnalysisManager(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _readContentRepo = unitOfWork.GetRepository<ReadCountOfTestAndContent>();
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
