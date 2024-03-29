﻿using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IRoleManager
    {
        #region Role Types
        RoleTypeDto GetRoleTypeDataRow(long roleId);
        Task<ServiceResult> GetRoleTypes();
        Task<ServiceResult> CreateRoleType(RoleTypeDto dto);
        Task<List<RoleTypeDto>> GetRoles();
        Task<ServiceResult> GetScreens();
        Task<ServiceResult> UpdateRolePermission(long roleTypeId, string menu, long userId);

        #endregion

        #region Role Permission

        Task<ServiceResult> GetRolePermission();
        Task<ServiceResult> CreateRolePermission(RolePermissionDto dto);

        #endregion
    }
}
