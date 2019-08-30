using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ServiceResult> UserLoginByMobil(string email, string password)
        {
            return await _userManager.UserLoginByMobil(email, password);
        }
    }
}