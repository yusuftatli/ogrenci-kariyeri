using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp.Areas.Api.Controller
{
    [Area("Api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        IAddressManager _addressManager;
        public AddressController(IAddressManager addressManager)
        {
            _addressManager = addressManager;
        }

        [Route("GetCities"), HttpGet]
        public async Task<ServiceResult> GetCities()
        {
            return await _addressManager.GetCities();
        }

        [HttpGet, Route("GetDistrict/{cityId}")]
        public async Task<ServiceResult> GetDistrict(int cityId)
        {
            return await _addressManager.GetDistrict(cityId);
        }

        [HttpGet, Route("GetDefaultValues")]
        public async Task<ServiceResult> DefaultValues()
        {
            return await _addressManager.DefaultValues();
        }

    }
}