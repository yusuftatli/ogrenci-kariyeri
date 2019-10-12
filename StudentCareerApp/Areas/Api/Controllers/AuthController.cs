using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Services;
using SCA.Services.Interface;

namespace StudentCareerApp.Areas.Api.Controller
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthManager _authManager;
        public AuthController(IAuthManager IauthManager)
        {
            _authManager = IauthManager;
        }

    }
}