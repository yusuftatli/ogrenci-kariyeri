using AutoMapper;
using AutoMapper.Configuration;
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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class AddressManager : IAddressManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Cities> _citiesRepo;
        private IGenericRepository<District> _districtRepo;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public AddressManager(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _citiesRepo = unitOfWork.GetRepository<Cities>();
            _districtRepo = unitOfWork.GetRepository<District>();
        }
        public async Task<ServiceResult> GetCities()
        {
            ServiceResult _res = new ServiceResult();
            List<CitiesDto> dataList = new List<CitiesDto>();
            try
            {
                string query = "select * from Cities";
                dataList = _db.Query<CitiesDto>(query).ToList();
                _res = Result.ReturnAsSuccess(data: dataList);
            }
            catch (Exception ex)
            {
                _res = Result.ReturnAsFail(message: "İl bilgileri çekilirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<ServiceResult> GetDistrict(int cityId)
        {
            ServiceResult _res = new ServiceResult();
            List<DistrictDto> dataList = new List<DistrictDto>();
            try
            {
                string query = "select Id as DistrictId,DistrictName,CityId from District where CityId = @cityId";
                var fitler = new DynamicParameters();
                fitler.Add("cityId", cityId);

                dataList = _db.Query<DistrictDto>(query, fitler).ToList();
                _res = Result.ReturnAsSuccess(data: dataList);
            }
            catch (Exception ex)
            {
                _res = Result.ReturnAsFail(message: "İlçe bilgileri çekilirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<List<CitiesDto>> CityList()
        {
            return _mapper.Map<List<CitiesDto>>(_citiesRepo.GetAll());
        }


    }
}
