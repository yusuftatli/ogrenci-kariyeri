using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Common;
using SCA.Common.Result;
using SCA.Entity.Dto;
using SCA.Entity.DTO;
using SCA.Entity.Model.Categories;
using SCA.Services;

namespace StudentCareerApp.Areas.Api.Controller
{
    [Area("Api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService<SCA.Entity.Entities.Category> _categoryService;
        public CategoryController(ICategoryManager categoryManager, ICategoryService<SCA.Entity.Entities.Category> categoryService)
        {
            _categoryService = categoryService;
        }

        #region Category

        // [Authorize()]
        [HttpGet("MainCategoryListWithParents")]
        public async Task<ServiceResult> MainCategoryListWithParents()
        {
            var res = await _categoryService.GetByWhereParams<MainCategoryDto>(x => x.IsActive == true);
            return res;
        }

        //[Authorize()]
        [HttpGet("MainCategoryList")]
        public async Task<ServiceResult> MainCategoryList(long? parentId)
        {
            var res = await _categoryService.GetByWhereParams<MainCategoryDto>(x => x.IsActive == true && x.ParentId == parentId.Value);
            return res;
        }

        //[Authorize()]
        [HttpPost("MainCategoryCreate")]
        public async Task<ServiceResult> MainCategoryCreate([FromBody]MainCategoryDto dto)
        {
            var user = HttpContext.GetSessionData<UserSession>("userInfo");
            var model = new CategoryInsertModel
            {
                CreatedDate = DateTime.Now,
                CreatedUserId = user.Id,
                Description = dto.Description,
                IsActive = true,
                ParentId = dto.ParentId
            };
            var res = await _categoryService.InsertAsync(model);
            return res;
        }

        [HttpPost("MainCategoryUpdate")]
        public async Task<ServiceResult> MainCategoryUpdate(MainCategoryDto dto)
        {
            var user = HttpContext.GetSessionData<UserSession>("userInfo");
            var model = new CategoryUpdateModel
            {
                Id = dto.Id,
                UpdatedDate = DateTime.Now,
                UpdatedUserId = user.Id,
                Description = dto.Description,
                IsActive = true,
                ParentId = dto.ParentId
            };
            var res = await _categoryService.UpdateAsync(model);
            return res;
        }

        [HttpPost("MainCategoryStatusUpdate")]
        public async Task<ServiceResult> MainCategoryStatusUpdate(int id, bool state)
        {
            var @params = new
            {
                Id = id,
                IsActive = state
            };
            var res = await _categoryService.UpdateAsync(@params);
            return res;
        }

        #endregion

    }
}