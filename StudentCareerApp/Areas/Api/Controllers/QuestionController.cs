using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class QuestionController : ControllerBase
    {
        IQuestionManager _questionManager;

        public QuestionController(IQuestionManager questionManager)
        {
            _questionManager = questionManager;
        }

        [HttpPost("CreateTest")]
        public async Task<ServiceResult> CreateTest([FromBody]QuestionCrudDto model)
        {
            _questionManager.CreateQuestion(model);
            return new ServiceResult
            {
                Data = model
            };
        }

    }
}