using AutoMapper;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using SCA.Services.Interface.InterfaceUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class B2CManagerUI : IB2CManagerUI
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitofWork;
        private readonly IGenericRepository<Content> _contentRepo;

        public B2CManagerUI(IUnitofWork unitofWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitofWork = unitofWork;
            _contentRepo = unitofWork.GetRepository<Content>();
        }

        public async Task<List<ContentForHomePageDTO>> GetContentsForHomePage()
        {
            var contents = _mapper.Map<List<ContentForHomePageDTO>>(_contentRepo.GetAll(x => x.IsDeleted.Equals(false)).ToList());
            return contents;
        }

    }
}
