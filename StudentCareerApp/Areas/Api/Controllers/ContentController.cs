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
using SCA.Entity.Enums;
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp.Areas.Api.Controller
{

    [Area("Api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        IContentManager _contentManager;
        ITagManager _tagManager;
        public ContentController(IContentManager contentManager, ITagManager tagManager)
        {
            _contentManager = contentManager;
            _tagManager = tagManager;
        }

        [HttpGet("GetTags")]
        public async Task<ServiceResult> GetTags()
        {
            return await _tagManager.GetTags();
        }

        //[Authorize()]
        [HttpPost("ContentShortList")]
        public async Task<ServiceResult> ContentShortList([FromBody]ContentSearchDto dto)
        {
            return await _contentManager.ContentShortList(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpGet("ContentList")]
        public async Task<ServiceResult> ContentList()
        {
            return await _contentManager.ContentList();
        }

        [HttpPost("ContentCreate")]
        public async Task<ServiceResult> ContentCreate([FromBody]ContentDto dto)
        {
            return await _contentManager.ContentCreate(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpDelete("ContentDelete")]
        public async Task<ServiceResult> ContentDelete(long Id)
        {
            return await _contentManager.ContentDelete(Id);
        }

        [HttpPost("UpdateAssayState")]
        public async Task<ServiceResult> UpdateAssayState(long Id, PublishState publishState)
        {
            return await _contentManager.UpdateAssayState(Id, publishState);
        }

        [HttpPost("UpdateContentPublish")]
        public async Task<ServiceResult> UpdateContentPublish([FromBody]publishStateDto dto)
        {
            return await _contentManager.UpdateContentPublish(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpGet("getContentbyid")]
        public async Task<ServiceResult> GetContent(long id)
        {
            return await _contentManager.GetContent(id);
        }
    }
}