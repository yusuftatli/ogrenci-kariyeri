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
    public class PagesController : ControllerBase
    {
        IPageManager _pageManager;
        public PagesController(IPageManager pageManager)
        {
            _pageManager = pageManager;
        }

        [HttpGet("web-getAllPage")]
        public async Task<ServiceResult> GetAllBasicPages()
        {
            return await _pageManager.GetAllBasicPages();
        }

        [HttpPost("web-createPage")]
        public async Task<ServiceResult> CreateBasicPage(BasicPagesDto dto)
        {
            return await _pageManager.CreateBasicPage(dto);
        }

        [HttpPost("web-updateState")]
        public async Task<ServiceResult> UpdateState(long id, bool state)
        {
            return await _pageManager.UpdateState(id, state);
        }
    }
}