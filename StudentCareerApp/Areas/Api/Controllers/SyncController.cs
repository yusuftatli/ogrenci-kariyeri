using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCA.Common.Result;
using SCA.Entity.Dto;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp.Areas.Api.Controller
{
    [Area("Api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        ISyncManager _syncManager;
        public SyncController(ISyncManager syncManager)
        {
            _syncManager = syncManager;
        }

        [HttpGet, Route("SyncAssay")]
        public async Task<ServiceResult> SyncAssay()
        {
            return await _syncManager.SyncAssay();
        }

        public async Task<ServiceResult> SyncDiger()
        {
            return await _syncManager.SyncDiger();
        }
    }
}