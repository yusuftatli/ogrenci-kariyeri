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

namespace Armut.Web.UI.Controllers
{
    [Area("Api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        #region Category

        [Authorize()]
        [HttpGet("MainCategoryListWithParents")]
        public async Task<ServiceResult> MainCategoryListWithParents()
        {
            return await _categoryManager.MainCategoryListWithParents();
        }

        [Authorize()]
        [HttpGet("MainCategoryList")]
        public async Task<ServiceResult> MainCategoryList(long? parentId)
        {
            return await _categoryManager.MainCategoryList(parentId);
        }

        [Authorize()]
        [HttpPost("MainCategoryCreate")]
        public async Task<ServiceResult> MainCategoryCreate([FromBody]MainCategoryDto dto)
        {
            return await _categoryManager.MainCategoryCreate(dto);
        }

        [HttpPost("MainCategoryUpdate")]
        public async Task<ServiceResult> MainCategoryUpdate(MainCategoryDto dto)
        {
            return await _categoryManager.MainCategoryUpdate(dto);
        }

        [HttpDelete]
        public async Task<ServiceResult> MainCategoryDelete(long id)
        {
            return await _categoryManager.MainCategoryDelete(id);
        }

        [HttpPost("MainCategoryStatusUpdate")]
        public async Task<ServiceResult> MainCategoryStatusUpdate(int id, bool state)
        {
            return await _categoryManager.MainCategoryStatusUpdate(id, state);
        }

        #endregion

    }
}