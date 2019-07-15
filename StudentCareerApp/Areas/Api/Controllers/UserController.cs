using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Services.Interface;

namespace Armut.Web.UI.Controllers
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

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<ServiceResult>> UpdateUser([FromBody]UsersDTO dto)
        {
            return await _userManager.UpdateUser(dto);
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<ActionResult<ServiceResult>> DeleteUser(long userId)
        {
            return await _userManager.DeleteUser(userId);
        }
    }
}