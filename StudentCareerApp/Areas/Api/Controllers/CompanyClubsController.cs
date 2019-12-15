using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SCA.Common;
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

        [HttpGet("web-get-allcompanies")]
        public async Task<ServiceResult> GetAllCompanies()
        {

            return await _companyClubsManager.GetAllCompaniesClubs(CompanyClupType.Company);
        }

        [HttpGet("web-get-allclubs")]
        public async Task<ServiceResult> GetAllClubs()
        {

            return await _companyClubsManager.GetAllCompaniesClubs(CompanyClupType.Club);
        }

        [HttpGet("web-get-companybyid")]
        public async Task<ServiceResult> GetCompanyId(string seoUrl)
        {
            return await _companyClubsManager.GetCompanyId(seoUrl);
        }

        [HttpGet("web-get-clubsbyid")]
        public async Task<ServiceResult> GetClubs(string seoUrl)
        {
            return await _companyClubsManager.GetCompanyId(seoUrl);
        }

        [HttpPost("web-create-company")]
        public async Task<ServiceResult> CreateCompany(CompanyClubsDto dto)
        {
            dto.CompanyClupType = CompanyClupType.Company;
            return await _companyClubsManager.CreateCompanyClubs(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [Consumes("multipart/form-data")]
        [HttpPost("web-create-clubs")]
        public async Task<ServiceResult> CreateClubs([FromForm]CompanyClubsDto dto)
        {
            dto.CompanyClupType = CompanyClupType.Club;
            return await _companyClubsManager.CreateCompanyClubs(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [Authorize()]
        [HttpGet("web-company-detail")]
        public async Task<ServiceResult> GetCompanyDetail(long id)
        {

            return await _companyClubsManager.GetCompanyDetail(id);
        }

        [HttpPost("follow-company")]
        public async Task<ServiceResult> FollowCompany(CompanyFollowDto dto)
        {
            ServiceResult res = new ServiceResult();
            if (HttpContext.GetSessionData<UserSession>("userInfo")?.Id > 0)
            {
                var userid = HttpContext.GetSessionData<UserSession>("userInfo").Id;
                return await _companyClubsManager.FollowCompany(userid, dto.seoUrl, dto.follow == "1" ? "0" : "1");
            }
            return Result.ReturnAsFail();
        }
    }
}