using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class MenuManager : IMenuManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IRoleManager _roleManager;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public MenuManager(IErrorManagement errorManagement, IRoleManager roleManager)
        {
            _errorManagement = errorManagement;
            _roleManager = roleManager;
        }


        public async Task<List<MenuDto>> GetMenus(long? userId)
        {
            List<MenuDto> _res = new List<MenuDto>();
            try
            {
                List<ScreenMasterDto> master = await GetMenuMaster(userId);
                List<ScreenDetailDto> detail = await GetMenuDetail(userId);

                if (master.Count > 0)
                {
                    foreach (var item in master)
                    {
                        MenuDto menu = new MenuDto();

                        menu.Id = item.Id;
                        menu.Name = item.Name;
                        menu.Icon = item.Icon;
                        menu.IsActive = item.IsActive;
                        menu.Detail = detail.Where(x => x.MasterId == item.Id).ToList();
                        _res.Add(menu);
                    }
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, userId, "GetMenus", PlatformType.Web);
            }
            return _res;
        }

        public async Task<List<ScreenMasterDto>> GetMenuMaster(long? userId)
        {
            List<ScreenMasterDto> listData = new List<ScreenMasterDto>();
            try
            {
                string query = "select * from ScreenMaster where IsActive = @IsActive;";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("IsActive", 1);
                listData = await _db.QueryAsync<ScreenMasterDto>(query, filter) as List<ScreenMasterDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, userId, "GetMaster", PlatformType.Web);
            }
            return listData;
        }

        public async Task<List<ScreenDetailDto>> GetMenuDetail(long? userId)
        {
            List<ScreenDetailDto> listData = new List<ScreenDetailDto>();
            try
            {
                string query = "select * from ScreenDetail where IsActive = @IsActive;";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("IsActive", 1);
                listData = await _db.QueryAsync<ScreenDetailDto>(query, filter) as List<ScreenDetailDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, userId, "GetMenuDetail", PlatformType.Web);
            }
            return listData;
        }

        public async Task<ServiceResult> SyncAllMenu(long roleTypeId, long userId)
        {
            ServiceResult _res = new ServiceResult();
            if (roleTypeId == 0)
            {
                _res = Result.ReturnAsFail(message: "Role boş geçilemez");
                return _res;
            }
            try
            {
                ServiceResult service = await GetRolePermission(roleTypeId, userId);
                List<RolePermissonListDto> rolePermissionDataList = service.Data as List<RolePermissonListDto>;

                List<ScreenMasterDto> master = await GetMenuMaster(0);
                List<ScreenDetailDto> detail = await GetMenuDetail(0);
                List<RolePermissionDto> ListData = new List<RolePermissionDto>();

                foreach (ScreenMasterDto _master in master)
                {
                    List<ScreenDetailDto> detailData = detail.Where(x => x.MasterId == _master.Id).ToList();
                    foreach (ScreenDetailDto _detail in detailData)
                    {
                        if (rolePermissionDataList.Count == 0)
                        {
                            RolePermissionDto role = new RolePermissionDto();
                            role.SreenMasterId = _master.Id;
                            role.IsActive = false;
                            role.RoleTypeId = roleTypeId;
                            role.ScreenDetailId = _detail.Id;
                            ListData.Add(role);
                        }
                        else
                        {
                            var data = rolePermissionDataList.Where(x => x.DetailId == _detail.Id).FirstOrDefault();
                            if (data == null)
                            {
                                RolePermissionDto role = new RolePermissionDto();
                                role.SreenMasterId = _master.Id;
                                role.IsActive = false;
                                role.RoleTypeId = roleTypeId;
                                role.ScreenDetailId = _detail.Id;
                                ListData.Add(role);
                            }
                        }
                    }
                }
                await CreateRolePermission(ListData);
                var listData = GetMenus(roleTypeId, userId);
                //string jsonData = JsonConvert.SerializeObject(listData);
                //await _roleManager.UpdateRolePermission(roleTypeId, jsonData, userId);
                _res = Result.ReturnAsSuccess(message: "Menu senkronizasyon işlemi başarıyla tamamlandı.", data: listData);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, userId, "SyncAllMenu", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Role yetkileri seknronizasyon sırasında hata meydana geldi.");
            }
            return _res;
        }


        public async Task<ServiceResult> GetRolePermission(long roleTypeId, long userId)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = string.Empty;
                query = @"select * from List_RolePermission where RoleTypeId = @RoleTypeId  order by DetailId asc";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("RoleTypeId", roleTypeId);

                var listData = await _db.QueryAsync<RolePermissonListDto>(query, filter) as List<RolePermissonListDto>;
                _res = Result.ReturnAsSuccess(data: listData);

            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, userId, "SyncAllMenu", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Menü yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<ServiceResult> GetMenus(long roleTypeId, long userId)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = $"select DISTINCT MasterId as Id, MasterName as Name, MasterIcon as Icon from List_RolePermission where RoleTypeId = {roleTypeId};";
                var screenMaster = await _db.QueryAsync<ScreenMasterDto>(query) as List<ScreenMasterDto>;

                ServiceResult service = await GetRolePermission(roleTypeId, userId);
                List<RolePermissonListDto> rolePermissionDataList = service.Data as List<RolePermissonListDto>;

                List<ScreenDoUI> ListData = new List<ScreenDoUI>();
                foreach (ScreenMasterDto master in screenMaster)
                {
                    ScreenDoUI _l = new ScreenDoUI();
                    _l.Id = master.Id;
                    _l.Name = master.Name;
                    _l.IsActive = master.IsActive;
                    _l.Icon = master.Icon;
                    foreach (RolePermissonListDto item in rolePermissionDataList.Where(x => x.MasterId == master.Id).ToList())
                    {
                        ScreenDetailDto _de = new ScreenDetailDto();
                        _de.Id = item.DetailId;
                        _de.Icon = item.DetailIcon;
                        _de.IsActive = item.IsActive;
                        _l.Details.Add(_de);
                    }
                    ListData.Add(_l);
                }
                _res = Result.ReturnAsSuccess(data: ListData);
            }
            catch (Exception ex)
            {

                await _errorManagement.SaveError(ex, userId, "GetMenus", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Menü yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<ServiceResult> CreateRolePermission(List<RolePermissionDto> dto)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = string.Empty;
                if (dto.Count > 0)
                {
                    foreach (RolePermissionDto item in dto)
                    {
                        DynamicParameters filter = new DynamicParameters();
                        if (item.Id == 0)
                        {
                            query = @"insert RolePermission (SreenMasterId, ScreenDetailId, IsActive, RoleTypeId, CreatedUserId, CreatedDate) values
                             (@SreenMasterId, @ScreenDetailId, @IsActive, @RoleTypeId, @CreatedUserId, @CreatedDate);";
                            filter.Add("CreatedUserId", 0);
                            filter.Add("CreatedDate", DateTime.Now);
                        }
                        else
                        {
                            query = @"Update RolePermission set SreenMasterId = @SreenMasterId, ScreenDetailId = @ScreenDetailId, IsActive = @IsActive, 
                            RoleTypeId = @RoleTypeId, UpdatedUserId = @UpdatedUserId, UpdatedDate = @UpdatedDate where Id = @Id";
                            filter.Add("Id", item.Id);
                            filter.Add("UpdatedUserId", 0);
                            filter.Add("UpdatedDate", DateTime.Now);
                        }

                        filter.Add("SreenMasterId", item.SreenMasterId);
                        filter.Add("ScreenDetailId", item.ScreenDetailId);
                        filter.Add("IsActive", item.IsActive);
                        filter.Add("RoleTypeId", item.RoleTypeId);

                        var result = await _db.ExecuteAsync(query, filter);
                    }
                }
                _res = Result.ReturnAsSuccess(message: "Kayıt İşlemi Başarılı.");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "SyncAllMenu", PlatformType.Web);
            }
            return _res;
        }

    }
}
