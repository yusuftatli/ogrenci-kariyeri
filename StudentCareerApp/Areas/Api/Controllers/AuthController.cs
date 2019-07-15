using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using SCA.Common.Result;
using SCA.Services.Interface;

namespace Armut.Web.UI.Controllers
{

    [Area("Api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthManager _authManager;
        public AuthController(IAuthManager IauthManager)
        {
            _authManager = IauthManager;
        }

        //[HttpPost("UserLogin")]
        //public async Task<ActionResult<ServiceResult>> UserLogin([FromBody]LoginModel dto)
        //{
        //    return await _authManager.UserLogin(dto);
        //}

        //[HttpPost, Route("login")]
        //public IActionResult Login([FromBody]LoginModel user)
        //{
        //    if (user == null)
        //    {
        //        return BadRequest("Invalid client request");
        //    }

        //    if (user.username == "1" && user.password == "1")
        //    {
        //        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
        //        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //        var tokeOptions = new JwtSecurityToken(
        //            issuer: "http://localhost:5000",
        //            audience: "http://localhost:5000",
        //            claims: new List<Claim>(),
        //            expires: DateTime.Now.AddMinutes(5),
        //            signingCredentials: signinCredentials
        //        );

        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        //        return Ok(new { Token = tokenString });
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}

        [HttpPost("PasswordForget/{emailAddress}")]
        public async Task<ActionResult<ServiceResult>> PasswordForget(string emailAddress)
        {
            return await _authManager.PasswordForget(emailAddress);
        }

        [HttpPost("ReNewPassword")]
        public async Task<ActionResult<ServiceResult>> ReNewPassword([FromBody]string guidValue)
        {
            return await _authManager.ReNewPassword(guidValue);
        }
    }
}