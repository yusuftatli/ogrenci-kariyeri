using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SCA.Common.Resource;
using SCA.Common.Result;
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
    public class RoleManager : IRoleManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public RoleManager(IErrorManagement errorManagement)
        {
            _errorManagement = errorManagement;
        }

        #region RoleType

        public RoleTypeDto GetRoleTypeDataRow(long roleId)
        {
            // var data = _mapper.Map<RoleTypeDto>(_roleTypeRepo.Get(x => x.Id == roleId));
            return null;
        }

        public async Task<ServiceResult> GetRoleTypes()
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from RoleType where  Id <> @Value";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Value", 1);

                var result = await _db.QueryAsync<RoleTypeDto>(query, filter) as List<RoleTypeDto>;
                _res = Result.ReturnAsSuccess(data: result);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetRoleTypes", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Rol Tipleri getirilirken hata meydana geldi");
            }
            return _res;
        }
        public async Task<List<RoleTypeDto>> GetRoles()
        {
            //var res = _mapper.Map<List<RoleTypeDto>>(_roleTypeRepo.GetAll(x => x.Id != 1));//süper admin geitrme
            return null;
        }

        public async Task<ServiceResult> CreateRoleType(RoleTypeDto dto)
        {
            string resultMessage = "";
            //  dto.IsActive = dto.isActiveVal;
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }

            //if (dto.Id == 0)
            //{
            //    _roleTypeRepo.Add(_mapper.Map<RoleType>(dto));
            //    resultMessage = "Kayıt işlemi başarılı";
            //}
            //else
            //{
            //    _roleTypeRepo.Update(_mapper.Map<RoleType>(dto));
            //    resultMessage = "Güncelleme işlemi başarılı";
            //}

            //_unitOfWork.SaveChanges();
            //var dataList = _roleTypeRepo.GetAll();
            return Result.ReturnAsSuccess(data: null);
        }

        #endregion

        #region RolePermission

        public async Task<ServiceResult> GetRolePermission()
        {
            //var dataList = _rolePermissionRepo.GetAll();
            return Result.ReturnAsSuccess(data: null);
        }

        public async Task<ServiceResult> CreateRolePermission(RolePermissionDto dto)
        {
            string resultMessage = "";
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }

            //if (dto.Id == 0)
            //{
            //    _rolePermissionRepo.Add(_mapper.Map<RolePermission>(dto));
            //    resultMessage = "Kayıt işlemi başarılı";
            //}
            //else
            //{
            //    _rolePermissionRepo.Update(_mapper.Map<RolePermission>(dto));
            //    resultMessage = "Güncelleme işlemi başarılı";
            //}

            //_unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(data: null);

        }

        public async Task<ServiceResult> GetScreens()
        {
            ServiceResult res = new ServiceResult();
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
                string value = JsonConvert.SerializeObject(ListData);
                res = Result.ReturnAsSuccess(data: value);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetScreens", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Ekran bilgileri getirilirken hata meydana geldi");
            }
            return res;
        }
        #endregion
    }
}
