using AutoMapper;
using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
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
    public class RoleManager : IRoleManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<RoleType> _roleTypeRepo;
        private IGenericRepository<RolePermission> _rolePermissionRepo;
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public RoleManager(IUnitofWork unitOfWork, IMapper mapper, IErrorManagement errorManagement)
        {
            _errorManagement = errorManagement;
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
                return Result.ReturnAsSuccess(null, null, dataList);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<List<RoleTypeDto>> GetRoles()
        {
            var res = _mapper.Map<List<RoleTypeDto>>(_roleTypeRepo.GetAll(x => x.Id != 1));//süper admin geitrme
            return res;
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
            return Result.ReturnAsSuccess(null, resultMessage, dataList);
        }

        #endregion

        #region RolePermission

        public async Task<ServiceResult> GetRolePermission()
        {
            var dataList = _rolePermissionRepo.GetAll();
            return Result.ReturnAsSuccess(null, null, dataList);
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
            return Result.ReturnAsSuccess(null, resultMessage, _rolePermissionRepo.GetAll());

        }

        public async Task<ServiceResult> GetScreens()
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from ScreenMaster";
                var screenMaster = await _db.QueryAsync<ScreenMasterDto>(query) as List<ScreenMasterDto>;

                query = "select * from ScreenDetail";
                List<ScreenDetailDto> screenDetail = await _db.QueryAsync<ScreenDetailDto>(query) as List<ScreenDetailDto>;

                List<ScreenDoUI> ListData = new List<ScreenDoUI>();
                foreach (ScreenMasterDto master in screenMaster)
                {
                    ScreenDoUI _l = new ScreenDoUI();
                    _l.Id = master.Id;
                    _l.Name = master.Name;
                    _l.IsActive = master.IsActive;
                    _l.Icon = master.Icon;
                    List<ScreenDetailDto> _d = screenDetail.Where(x => x.MasterId == master.Id).ToList();
                    _l.Details = _d;
                    ListData.Add(_l);
                }
                _res = Result.ReturnAsSuccess(data: ListData);

            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: "Ekran bilgileri getirilirken hata meydana geldi");
            }
            return _res;
        }
        #endregion
    }
}
