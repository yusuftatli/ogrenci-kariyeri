using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class SettingsController : ControllerBase
    {
        ISettingsManager _settingsManager;
        ITagManager _tagManager;
        ICommentManager _commentManager;
        public SettingsController(IContentManager contentManager, ITagManager tagManager, ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            _tagManager = tagManager;
        }


        [HttpGet("get-settings")]
        public async Task<ServiceResult> GetSettingValue()
        {
            return await _settingsManager.GetSettingValue();
        }

        [HttpPost("set-settings")]
        public async Task<ServiceResult> SetSettingsValue([FromBody]MultipleCountDto dto)
        {
            return await _settingsManager.SetSettingsValue(dto);
        }

        [HttpGet("settings-multipleReadCount-only")]
        public async Task<ServiceResult> GetContentMultipleCountOnly(long id)
        {
            return await _settingsManager.GetContentMultipleCountOnly(id);
        }

        [HttpPost("settings-setreadcount-only")]
        public async Task<ServiceResult> SetContentMultipleCountOnly([FromBody]MultipleCountDto dto)
        {
            return await _settingsManager.SetContentMultipleCountOnly(dto);
        }
    }
}