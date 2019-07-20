using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IQuestionManager
    {
        Task<ServiceResult> CreateQuestion(QuestionCrudDto dto);

    }
}
