using AutoMapper;
using SCA.Common.Resource;
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
    public class RoleManager : IRoleManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<RoleType> _roleTypeRepo;
        private IGenericRepository<RolePermission> _rolePermissionRepo;

        public RoleManager(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleTypeRepo = unitOfWork.GetRepository<RoleType>();
            _rolePermissionRepo = unitOfWork.GetRepository<RolePermission>();
        }



        #region RoleType

        public RoleTypeDto GetRoleTypeDataRow(long roleId)
        {
            var data = _mapper.Map<RoleTypeDto>(_roleTypeRepo.Get(x => x.Id == roleId));
            return data;
        }

        public async Task<ServiceResult> GetRoleTypes()
        {
            try
            {
                var dataList = _mapper.Map<List<RoleTypeDto>>(_roleTypeRepo.GetAll(x => x.IsDeleted.Equals(false)));
                return Result.ReturnAsSuccess(null, dataList);

            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task<ServiceResult> CreateRoleType(RoleTypeDto dto)
        {
            string resultMessage = "";
            dto.IsActive = dto.isActiveVal;
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }

            if (dto.Id == 0)
            {
                _roleTypeRepo.Add(_mapper.Map<RoleType>(dto));
                resultMessage = "Kayıt işlemi başarılı";
            }
            else
            {
                _roleTypeRepo.Update(_mapper.Map<RoleType>(dto));
                resultMessage = "Güncelleme işlemi başarılı";
            }

            _unitOfWork.SaveChanges();
            var dataList = _roleTypeRepo.GetAll();
            return Result.ReturnAsSuccess(resultMessage, dataList);
        }

        #endregion

        #region RolePermission

        public async Task<ServiceResult> GetRolePermission()
        {
            var dataList = _rolePermissionRepo.GetAll();
            return Result.ReturnAsSuccess(null, dataList);
        }

        public async Task<ServiceResult> CreateRolePermission(RolePermissionDto dto)
        {
            string resultMessage = "";
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }

            if (dto.Id == 0)
            {
                _rolePermissionRepo.Add(_mapper.Map<RolePermission>(dto));
                resultMessage = "Kayıt işlemi başarılı";
            }
            else
            {
                _rolePermissionRepo.Update(_mapper.Map<RolePermission>(dto));
                resultMessage = "Güncelleme işlemi başarılı";
            }

            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(resultMessage, _rolePermissionRepo.GetAll());

        }

        #endregion
    }
}
