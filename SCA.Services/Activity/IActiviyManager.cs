using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IActivityManager
    {
        Task<ServiceResult> ActivityCreate(ContentDto dto, UserSession session);
        Task<ServiceResult> ActivityShortList(ContentSearchDto dto, UserSession session);
        Task<ServiceResult> CreateActivityMotion(ActivityMotionDto dto);
        string PassComplexData();
    }
}
