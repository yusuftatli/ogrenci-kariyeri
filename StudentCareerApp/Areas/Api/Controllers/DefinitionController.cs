using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SCA.Common;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp.Areas.Api.Controller
{

    [Area("Api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class DefinitionController : ControllerBase
    {
        IDefinitionManager _definitionManager;
        public DefinitionController(IDefinitionManager definitionManager)
        {
            _definitionManager = definitionManager;
        }


        #region Department
        [Authorize]
        [HttpGet("education-getdepartment")]
        public async Task<ServiceResult> GetDepartment()
        {

            return await _definitionManager.GetDepartment(await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpGet("getdepartment")]
        public async Task<ServiceResult> GetDepartment1()
        {

            return await _definitionManager.GetDepartmentForUI1();
        }

        [HttpPost("education-cretadepartment")]
        public async Task<ServiceResult> CreateDepartment([FromBody] DepartmentDto dto)
        {
            return await _definitionManager.CreateDepartment(dto, await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpPost("education-Update-DepartmentIsActive")]
        public async Task<ServiceResult> UpdateDepartmentIsActive(long Id, bool IsActive)
        {
            return await _definitionManager.UpdateDepartmentIsActive(Id, IsActive);
        }
        #endregion

        #region Faculty
        [HttpGet("education-getfaculty")]
        public async Task<ServiceResult> GetFaculty()
        {
            return await _definitionManager.GetFaculty(await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpPost("education-createfaculty")]
        public async Task<ServiceResult> CreateFaculty(FacultyDto dto)
        {
            return await _definitionManager.CreateFaculty(dto, await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpPost("education-Update-FacultIsActive")]
        public async Task<ServiceResult> UpdateFacultIsActive(long Id, bool IsActive)
        {
            return await _definitionManager.UpdateFacultIsActive(Id, IsActive);
        }
        #endregion

        #region High Schooll
        [HttpGet("education-gethighschool")]
        public async Task<ServiceResult> GetHighSchool()
        {
            return await _definitionManager.GetHighSchool(await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpPost("education-createhighschool")]
        public async Task<ServiceResult> CreateHighSchool([FromBody]HighSchoolDto dto)
        {
            return await _definitionManager.CreateHighSchool(dto, await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpPost("education-Update-highSchoolIsActive")]
        public async Task<ServiceResult> UpdatehighSchoolIsActive(long Id, bool IsActive)
        {
            return await _definitionManager.UpdatehighSchoolIsActive(Id, IsActive);
        }
        #endregion

        #region Student Class
        [HttpGet("education-getstudentclass")]
        public async Task<ServiceResult> GetStudentClass()
        {
            return await _definitionManager.GetStudentClass(await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpPost("education-createstudentclass")]
        public async Task<ServiceResult> CreateStudentClass(StudentClassDto dto)
        {
            return await _definitionManager.CreateStudentClass(dto, await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpPost("education-Update-StudentClassIsActive")]
        public async Task<ServiceResult> UpdateStudentClassIsActive(long Id, bool IsActive)
        {
            return await _definitionManager.UpdateStudentClassIsActive(Id, IsActive);
        }
        #endregion

        #region University
        [HttpGet("education-getuniversity")]
        public async Task<ServiceResult> GetUniversity()
        {
            return await _definitionManager.GetUniversity(await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpPost("education-createuniversity")]
        public async Task<ServiceResult> CreateUniversity(UniversityDto dto)
        {
            return await _definitionManager.CreateUniversity(dto, await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpPost("education-Update-UniversityIsActive")]
        public async Task<ServiceResult> UpdateUniversityIsActive(long Id, bool IsActive)
        {
            return await _definitionManager.UpdateUniversityIsActive(Id, IsActive);
        }
        #endregion

        #region Sector
        [HttpGet("getallsector")]
        public async Task<ServiceResult> GetAllSector()
        {
            return await _definitionManager.GetAllSector(await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpPost("createsektor")]
        public async Task<ServiceResult> CreateSector([FromBody]SectorDto dto)
        {
            return await _definitionManager.CreateSector(dto, await HttpContext.GetTokenAsync("access_token"));
        }
        #endregion

        #region Title
        [HttpGet("getTitles")]
        public async Task<ServiceResult> GetTitle()
        {
            return await _definitionManager.GetTitle();
        }

        [HttpPost("createTitle")]
        public async Task<ServiceResult> CreateTitle([FromBody]TitleDto dto)
        {
            return await _definitionManager.CreateTitle(dto);
        }
        #endregion


    }
}