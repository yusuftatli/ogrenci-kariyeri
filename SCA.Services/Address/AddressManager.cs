using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common.Base;
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
    public class AddressManager : BaseClass, IAddressManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection(ConnectionString1);

        public AddressManager(IErrorManagement errorManagement)
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


        public async Task<ServiceResult> DefaultValues()
        {
            ServiceResult res = new ServiceResult();
            try
            {
                DefaultDatalarMobilDto resultModel = new DefaultDatalarMobilDto();

                string query1 = "";
                string query2 = "";
                string query3 = "";
                string query4 = "";
                string query5 = "";

                query1 = "select * from HighSchool";
                query2 = "select * from Universities order by Description asc";
                query3 = "select * from Category where IsActive = 1";
                query4 = "select * from Departmnet  order by Description asc";
                query5 = "select Id as Id, CityName As Description  from Cities";

                resultModel.HighSchoolList = await _db.QueryAsync<HighSchoolMobilDto>(query1) as List<HighSchoolMobilDto>;
                resultModel.UniversityList = await _db.QueryAsync<UniverstiyMobilDto>(query2) as List<UniverstiyMobilDto>;
                resultModel.CategoriesList = await _db.QueryAsync<CategoriesMobilDto>(query3) as List<CategoriesMobilDto>;
                resultModel.DepartmentList = await _db.QueryAsync<DepartmentMobilDto>(query4) as List<DepartmentMobilDto>;
                resultModel.CityList = await _db.QueryAsync<CitiesMobilDto>(query5) as List<CitiesMobilDto>;

                res = Result.ReturnAsSuccess(data: resultModel);
            }
            catch (Exception ex)
            {
                res = Result.ReturnAsFail();
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
                await _errorManagement.SaveError(ex, 0, "CityList ", Entity.Enums.PlatformType.Web);
            }
            return res;
        }


    }
}
