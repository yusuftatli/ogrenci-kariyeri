using System;
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
    public class RolesController : ControllerBase
    {
        IRoleManager _roleManager;
        IMenuManager _menuManager;

        public RolesController(IRoleManager roleManager,
            IMenuManager menuManager)
        {
            _roleManager = roleManager;
            _menuManager = menuManager;
        }

        #region Role Types

        [HttpGet, Route("menu-GetMenuWitRoles")]
        public async Task<ServiceResult> GetRolePermission(long roleTypeId)
        {
            return await _menuManager.GetRolePermission(roleTypeId, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")).Id);
        }

        [HttpPost, Route("menu-syncMenu")]
        public async Task<ServiceResult> GetRoleTypes(long id)
        {
            return await _menuManager.SyncAllMenu(id, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")).RoleTypeId);
        }

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

        [HttpGet("role-GetScreens")]
        public async Task<ServiceResult> GetScreens()
        {
            return await _roleManager.GetScreens();
        }
    }
}