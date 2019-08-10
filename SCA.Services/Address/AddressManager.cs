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
    public class AddressManager : IAddressManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Cities> _citiesRepo;
        private IGenericRepository<District> _districtRepo;
        

        public AddressManager(IUnitofWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _citiesRepo = unitOfWork.GetRepository<Cities>();
            _districtRepo = unitOfWork.GetRepository<District>();
            
        }
        public async Task<ServiceResult> GetCities()
        {
            var data = _mapper.Map<List<CitiesDto>>(_citiesRepo.GetAll());
            return Result.ReturnAsSuccess(null, data);
        }

        public async Task<ServiceResult> GetDistrict(int cityId)
        {
            if (cityId.Equals(null))
            {
                return Result.ReturnAsFail(AlertResource.OperationFailed, null);
            }
            var data = _mapper.Map<List<DistrictDto>>(_districtRepo.GetAll(x => x.CityId == cityId));
            return Result.ReturnAsSuccess(null, data);
        }


        public List<CitiesDto> CityList()
        {
            return _mapper.Map<List<CitiesDto>>(_citiesRepo.GetAll());
        }
       

    }
}
