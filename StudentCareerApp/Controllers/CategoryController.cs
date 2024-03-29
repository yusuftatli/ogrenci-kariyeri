﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Entity.Entities;
using SCA.Entity.SPModels.SPResult;

namespace SCA.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IContentService<Content> _contentService;

        public CategoryController(IContentService<Content> contentService)
        {
            _contentService = contentService;
        }

        [Route("Kategoriler/{seoUrl}-{id}"), HttpGet]
        public async Task<IActionResult> Index(string id, string seoUrl)
        {
            var @params = new
            {
                categoryId = id,
                count = 60,
                pageNumber = 0
            };
            ViewBag.CategoryId = id;
            var res = await _contentService.SPQueryAsync<object, GetCategoryContents>(@params);
            var res2 = res.Data as List<GetCategoryContents>;
            return View(res2);
        }

        public async Task<IActionResult> GetNewsWith3ColumnsData(string id, int count, int offset)
        {
            var @params = new
            {
                categoryId = id,
                count,
                pageNumber = offset
            };
            var res = await _contentService.SPQueryAsync<object, GetCategoryContents>(@params);
            return View("~/Views/Shared/Components/NewsWith3Columns/_NewsWith3Columns.cshtml", res.Data);
        }
    }
}