using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Services.Interface;

namespace SCA.Web.Api.Controllers
{
    [Area("Api")]
    [Route("[area]/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        IQuestionManager _questionManager;

        public QuestionController(IQuestionManager questionManager)
        {
            _questionManager = questionManager;
        }

        [HttpGet("GetTests")]
        public IActionResult GetTests()
        {
            return View();
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