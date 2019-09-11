﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [HttpGet, Route("education-getdepartment")]
        public async Task<ServiceResult> GetDepartment()
        {
            return await _definitionManager.GetDepartment(JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpPost, Route("education-cretadepartment")]
        public async Task<ServiceResult> CreateDepartment([FromBody] DepartmentDto dto)
        {
            return await _definitionManager.CreateDepartment(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpPost, Route("education-Update-DepartmentIsActive")]
        public async Task<ServiceResult> UpdateDepartmentIsActive(long Id, bool IsActive)
        {
            return await _definitionManager.UpdateDepartmentIsActive(Id, IsActive);
        }
        #endregion

        #region Faculty
        [HttpGet, Route("education-getfaculty")]
        public async Task<ServiceResult> GetFaculty()
        {
            return await _definitionManager.GetFaculty(JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpPost, Route("education-createfaculty")]
        public async Task<ServiceResult> CreateFaculty(FacultyDto dto)
        {
            return await _definitionManager.CreateFaculty(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpPost, Route("education-Update-FacultIsActive")]
        public async Task<ServiceResult> UpdateFacultIsActive(long Id, bool IsActive)
        {
            return await _definitionManager.UpdateFacultIsActive(Id, IsActive);
        }
        #endregion

        #region High Schooll
        [HttpGet, Route("education-gethighschool")]
        public async Task<ServiceResult> GetHighSchool()
        {
            return await _definitionManager.GetHighSchool(JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpPost, Route("education-createhighschool")]
        public async Task<ServiceResult> CreateHighSchool([FromBody]HighSchoolDto dto)
        {
            return await _definitionManager.CreateHighSchool(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpPost, Route("education-Update-highSchoolIsActive")]
        public async Task<ServiceResult> UpdatehighSchoolIsActive(long Id, bool IsActive)
        {
            return await _definitionManager.UpdatehighSchoolIsActive(Id, IsActive);
        }
        #endregion

        #region Student Class
        [HttpGet, Route("education-getstudentclass")]
        public async Task<ServiceResult> GetStudentClass()
        {
            return await _definitionManager.GetStudentClass(JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpPost, Route("education-createstudentclass")]
        public async Task<ServiceResult> CreateStudentClass(StudentClassDto dto)
        {
            return await _definitionManager.CreateStudentClass(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpPost, Route("education-Update-StudentClassIsActive")]
        public async Task<ServiceResult> UpdateStudentClassIsActive(long Id, bool IsActive)
        {
            return await _definitionManager.UpdateStudentClassIsActive(Id, IsActive);
        }
        #endregion

        #region University
        [HttpGet, Route("education-getuniversity")]
        public async Task<ServiceResult> GetUniversity()
        {
            return await _definitionManager.GetUniversity(JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpPost, Route("education-createuniversity")]
        public async Task<ServiceResult> CreateUniversity(UniversityDto dto)
        {
            return await _definitionManager.CreateUniversity(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpPost, Route("education-Update-UniversityIsActive")]
        public async Task<ServiceResult> UpdateUniversityIsActive(long Id, bool IsActive)
        {
            return await _definitionManager.UpdateUniversityIsActive(Id, IsActive);
        }
        #endregion

        #region Sector
        [HttpGet, Route("getallsector")]
        public async Task<ServiceResult> GetAllSector()
        {
            return await _definitionManager.GetAllSector(JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpPost, Route("createsektor")]
        public async Task<ServiceResult> CreateSector([FromBody]SectorDto dto)
        {
            return await _definitionManager.CreateSector(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }
        #endregion

    }
}