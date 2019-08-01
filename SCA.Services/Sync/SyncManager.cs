using AutoMapper;
using SCA.Common.Result;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class SyncManager : ISyncManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Content> _contentRepo;

        public SyncManager(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _contentRepo = unitOfWork.GetRepository<Content>();
        }

        public Task<ServiceResult> SyncAssay()
        {

        }
    }
}
