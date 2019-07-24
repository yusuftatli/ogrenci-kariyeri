using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp.Areas.Api.Controller
{
    [Area("Admin")]
    [Route("[area]/[controller]")]
    public class ScreenController : ControllerBase
    {
        IMenuManager _menuManager;
        public ScreenController(IMenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        [HttpPost, Route("create-screen-master")]
        public async Task<ServiceResult> CreateScreenMaster(ScreenMaster dto)
        {
            return await _menuManager.CreateScreenMaster(dto);
        }

        [HttpGet, Route("changeState-screen-master")]
        public async Task<ServiceResult> ScreenMasterState(long id, bool state)
        {
            return await _menuManager.ScreenMasterState(id, state);
        }

        [HttpPost, Route("create-screen-detail")]
        public async Task<ServiceResult> CreateScreenDetail(ScreenDetailDto dto)
        {
            return await _menuManager.CreateScreenDetail(dto);
        }

        [HttpGet, Route("changeState-screen-detail")]
        public async Task<ServiceResult> ScreenDetailState(long id, bool state)
        {
            return await _menuManager.ScreenDetailState(id, state);
        }
    }
}