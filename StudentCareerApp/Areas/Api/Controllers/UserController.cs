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
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp.Areas.Api.Controller
{

    [Area("Api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [Authorize()]
        [HttpPost("user-forgetpassword")]
        public async Task<ServiceResult> PasswordRenew(string emailAddress)
        {
            return await _userManager.PasswordRenew(emailAddress, await HttpContext.GetTokenAsync("access_token"));
        }

        //[Authorize()]
        [HttpPost("user-create-web")]
        public async Task<ServiceResult> CreateUserByWeb(UserWeblDto dto)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            return await _userManager.CreateUserByWeb(dto, await HttpContext.GetTokenAsync("access_token"));
        }

        [HttpGet("web-getinfo")]
        public async Task<ServiceResult> GetUserInfo()
        {
            return await _userManager.GetUserInfo(Convert.ToInt64(JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")).Id));
        }

        [HttpGet("web-getallusers")]
        public async Task<List<UserModelList>> GetUserList()
        {
            return await _userManager.GetUserList();
        }


        [HttpPost("update-user-statu")]
        public async Task<ServiceResult> UpdateUserStatu(long id, bool value)
        {
            return await _userManager.UpdateUserStatu(id, value);
        }

        /// <summary>
        /// mobil kullanıcı kayıt
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("mobil-createuser")]
        public async Task<ServiceResult> CreateUserByMobil(UserMobilDto dto)
        {
            return await _userManager.CreateUserByMobil(dto);
        }

        [HttpPost("mobil-UpdateUserByWeb")]
        public async Task<ServiceResult> UpdateUserByWeb(UserWeblDto dto)
        {
            return await _userManager.UpdateUserByWeb(dto);
        }

        /// <summary>
        /// mobil kullanıcı login
        /// </summary>
        /// <param name="email">email Adress</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("mobil-userlogin")]
        public async Task<ServiceResult> UserLoginByMobil(MobilUserLoginDto dto)
        {
            var res = await _userManager.UserLoginByMobil(dto);

            if (res.IsSuccess())
            {
                HttpContext.Session.SetString("userInfo", Newtonsoft.Json.JsonConvert.SerializeObject(res.Data));
                JsonSerializer serializer = new JsonSerializer();
                var result = JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo"));
                HttpContext.Session.SetString("NameSurname", result.Name + " " + result.Surname);
                HttpContext.Session.SetString("RoleTypeId", result.RoleTypeId.ToString());
                return res;
            }
            return res;
        }

        [HttpPost("web-updateUserRoleType")]
        public async Task<ServiceResult> UpdateUserRoleType(UserRoleTypeDto dto)
        {
            return await _userManager.UpdateUserRoleType(dto, JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }

        [HttpGet("web-token")]
        public async Task<ServiceResult> WebToken()
        {
            return Result.ReturnAsSuccess(data: JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")).Token);
        }

        [HttpGet("get-dashboard")]
        public async Task<ServiceResult> Dashboard()
        {
            return await _userManager.Dashboard(JsonConvert.DeserializeObject<UserSession>(HttpContext.Session.GetString("userInfo")));
        }
    }
}