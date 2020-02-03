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
    public class ActivityController : ControllerBase
    {
        IActivityManager _activityManager;
        ITagManager _tagManager;
        ICommentManager _commentManager;
        public ActivityController(IActivityManager activityManager, ITagManager tagManager, ICommentManager commentManager)
        {
            _activityManager = activityManager;
            _tagManager = tagManager;
            _commentManager = commentManager;
        }

        [HttpPost("post")]
        public async Task<ServiceResult> ActivityCreate(ContentDto dto)
        {
            return await _activityManager.ActivityCreate(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpGet("get")]
        public async Task<ServiceResult> ActivityShortList(ContentSearchDto dto)
        {
            return await _activityManager.ActivityShortList(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));

        }


    }
}