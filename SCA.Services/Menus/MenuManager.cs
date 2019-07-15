using AutoMapper;
using SCA.Common.Result;
using SCA.Entity.DTO;
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
    public class MenuManager : IMenuManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<MenuList> _menuListRepo;

        public MenuManager(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _menuListRepo = unitOfWork.GetRepository<MenuList>();
        }


        public async Task<ServiceResult> AddMasterMenu(MenuListDto dto)
        {
            if (dto.Equals(null))
            {
                return Result.ReturnAsFail();
            }

            _menuListRepo.Add(_mapper.Map<MenuList>(dto));
            return _unitOfWork.SaveChanges();
        }



    }
}
