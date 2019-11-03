﻿using System;
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
    public class ContentController : ControllerBase
    {
        IContentManager _contentManager;
        ITagManager _tagManager;
        ICommentManager _commentManager;
        public ContentController(IContentManager contentManager, ITagManager tagManager, ICommentManager commentManager)
        {
            _contentManager = contentManager;
            _tagManager = tagManager;
            _commentManager = commentManager;
        }

        [Authorize()]
        [HttpPost("content-create-comment")]
        public async Task<ServiceResult> CreateCommentsByMobil(CommentMobilDto dto)
        {
            return await _commentManager.CreateCommentsByMobil(dto, await HttpContext.GetTokenAsync("access_token"));
        }


        [Authorize()]
        [HttpGet("content-list-favori")]
        public async Task<ServiceResult> GetFavoriteContents(int count)
        {
            return await _contentManager.GetFavoriteContents(count, await HttpContext.GetTokenAsync("access_token"));
        }


        [Authorize()]
        [HttpGet("content-favori-list")]
        public async Task<ServiceResult> ContentShortListFavoriByMobil(int count)
        {
            return await _contentManager.ContentShortListFavoriByMobil(count, await HttpContext.GetTokenAsync("access_token"));
        }

        [Authorize()]
        [HttpPost("content-create-favori")]
        public async Task<ServiceResult> CreateFavorite(FavoriteMobilDto dto)
        {
            return await _contentManager.CreateFavorite(dto, await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpGet("GetSearch")]
        public async Task<ServiceResult> GetSearch(string value)
        {
            return await _contentManager.GetSearch(value);
        }

        [HttpGet("GetTags")]
        public async Task<ServiceResult> GetTags()
        {
            return await _tagManager.GetTags();
        }

        [Authorize()]
        [HttpPost("content-shotlist-mobil")]
        public async Task<ServiceResult> ContentShortListByMobil(ContentSearchByMoilDto dto)
        {
            return await _contentManager.ContentShortListByMobil(dto, await HttpContext.GetTokenAsync("access_token"));
        }

        [Authorize()]
        [HttpPost("content-Detail-mobil")]
        public async Task<ServiceResult> GetContentByMobil(ContentDetailMobilDto dto)
        {
            return await _contentManager.GetContentByMobil(dto, await HttpContext.GetTokenAsync("access_token"));
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

        [HttpGet("GetAllTags")]
        public async Task<ServiceResult> GetAllTags()
        {
            return await _contentManager.GetAllTags();
        }

        [HttpGet("comment-GetAllCommentsPendingApproval")]
        public async Task<ServiceResult> GetAllCommentsPendingApproval(int readType)
        {
            return await _commentManager.GetAllCommentsPendingApproval(readType);
        }


        [HttpGet("comment-approve")]
        public async Task<ServiceResult> ApproveComment(long id)
        {
            return await _commentManager.ApproveComment(id);
        }

        [HttpGet("comment-Dashboard")]
        public async Task<ServiceResult> Dashboard()
        {
            return await _commentManager.Dashboard();
        }


    }
}