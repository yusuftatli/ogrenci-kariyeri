﻿using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IAddressManager
    {
        Task<List<CitiesDto>> CityList();
        Task<ServiceResult> GetCities();
        Task<ServiceResult> GetDistrict(int cityId);


        Task<ServiceResult> DefaultValues();


    }
}
