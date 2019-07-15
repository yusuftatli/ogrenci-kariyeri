using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Services.Interface;

namespace Armut.Web.UI.Controllers
{

    [Area("Api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        IRoleManager _roleManager;
        public RolesController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        #region Role Types

        [HttpGet, Route("role-getroletypes")]
        public async Task<ServiceResult> GetRoleTypes()
        {
            return await _roleManager.GetRoleTypes();
        }

        [HttpPost, Route("role-createroletype")]
        public async Task<ServiceResult> CreateRoleType(RoleTypeDto dto)
        {
            return await _roleManager.CreateRoleType(dto);
        }
        #endregion

        #region Role Permission

        [Route("/rolepermission-GetRolePermission")]
        [HttpGet]
        public async Task<ServiceResult> GetRolePermission()
        {
            return await _roleManager.GetRolePermission();
        }
        public async Task<ServiceResult> CreateRolePermission(RolePermissionDto dto)
        {
            return await _roleManager.CreateRolePermission(dto);
        }

        #endregion
    }
}