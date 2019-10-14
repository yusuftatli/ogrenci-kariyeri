using Dapper;
using MySql.Data.MySqlClient;
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
    public class MenuManager: IMenuManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public MenuManager(IErrorManagement errorManagement)
        {
            _errorManagement = errorManagement;
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
                await _errorManagement.SaveError(ex, userId, "GetMenus", PlatformType.Mobil);
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
                await _errorManagement.SaveError(ex, userId, "GetMaster", PlatformType.Mobil);
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
                await _errorManagement.SaveError(ex, userId, "GetMenuDetail", PlatformType.Mobil);
            }
            return listData;
        }
    }
}
