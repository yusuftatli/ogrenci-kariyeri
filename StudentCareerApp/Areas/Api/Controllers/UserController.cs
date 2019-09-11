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

        [HttpPost("CreateUser")]
        public async Task<ActionResult<ServiceResult>> CreateUser([FromBody]UsersDTO dto)
        {
            return await _userManager.CreateUser(dto);
        }

        [HttpGet("web-getallusers")]
        public async Task<List<UserModelList>> GetUserList()
        {
            return await _userManager.GetUserList();
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<ActionResult<ServiceResult>> DeleteUser(long userId)
        {
            return await _userManager.DeleteUser(userId);
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
        [HttpPost("mobil-createuser")]
        public async Task<ServiceResult> CreateUserByMobil(UserMobilDto dto)
        {
            return await _userManager.CreateUserByMobil(dto);
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
            return await _userManager.UserLoginByMobil(dto);
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