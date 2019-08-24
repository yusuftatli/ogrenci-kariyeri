using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp.Areas.Api.Controller
{

    [Area("Api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class CompanyClubsController : ControllerBase
    {
        ICompanyClubManager _companyClubsManager;
        public CompanyClubsController(ICompanyClubManager companyClubsManager)
        {
            _companyClubsManager = companyClubsManager;
        }

        [HttpGet("web-companyclubs")]
        public async Task<ServiceResult> GetAllCompaniesClubs(CompanyClupType companyClupType)
        {
            return await _companyClubsManager.GetAllCompaniesClubs(companyClupType);
        }

        [HttpGet("web-get-company")]
        public async Task<ServiceResult> GetCompanyId(long id)
        {
            return await _companyClubsManager.GetCompanyId(id);
        }

        [HttpGet("web-get-clubs")]
        public async Task<ServiceResult> GetClubs(long id)
        {
            return await _companyClubsManager.GetCompanyId(id);
        }

        [HttpPost("web-create-company")]
        public async Task<ServiceResult> CreateCompany(CompanyClubsDto dto)
        {
            dto.CompanyClupType = CompanyClupType.Company;
            long userId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            return await _companyClubsManager.CreateCompanyClubs(dto, userId);
        }

        [Consumes("multipart/form-data")]
        [HttpPost("web-create-clubs")]
        public async Task<ServiceResult> CreateClubs([FromForm]CompanyClubsDto dto)
        {
            dto.CompanyClupType = CompanyClupType.Club;
            long userId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));
            return await _companyClubsManager.CreateCompanyClubs(dto, userId);
        }
    }
}