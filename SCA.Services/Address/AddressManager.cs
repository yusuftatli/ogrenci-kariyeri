using AutoMapper;
using AutoMapper.Configuration;
using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class AddressManager : IAddressManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public AddressManager( IErrorManagement errorManagement)
        {
            _errorManagement = errorManagement;
        }
        public async Task<ServiceResult> GetCities()
        {
            ServiceResult res = new ServiceResult();
            List<CitiesDto> dataList = new List<CitiesDto>();
            try
            {
                string query = "select Id as CityId, CityName as CityName from Cities";
                dataList = _db.Query<CitiesDto>(query).ToList();
                res = Result.ReturnAsSuccess(data: dataList);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "GetCities", Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail(message: "İl bilgileri çekilirken hata meydana geldi");
            }
            return res;
        }

        public async Task<ServiceResult> GetDistrict(int cityId)
        {
            ServiceResult res = new ServiceResult();
            List<DistrictDto> dataList = new List<DistrictDto>();
            try
            {
                string query = "select Id as DistrictId,DistrictName,CityId from District where CityId = @cityId";
                var fitler = new DynamicParameters();
                fitler.Add("cityId", cityId);

                dataList = _db.Query<DistrictDto>(query, fitler).ToList();
                res = Result.ReturnAsSuccess(data: dataList);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "GetDistrict " + cityId, Entity.Enums.PlatformType.Web);
                res = Result.ReturnAsFail(message: "İlçe bilgileri çekilirken hata meydana geldi");
            }
            return res;
        }

        public async Task<List<CitiesDto>> CityList()
        {
            List<CitiesDto> res = new List<CitiesDto>();
            try
            {
                string query = "select * from Cities";
                res = _db.Query<CitiesDto>(query).ToList();
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "CityList " , Entity.Enums.PlatformType.Web);
            }
            return res;
        }


    }
}
