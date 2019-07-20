using SCA.Common.Result;
using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IAnalysisManager
    {
        Task<ServiceResult> LogReadTestOrContent(ReadType readType, PlatformType platformType, long id);
        long GetCountValue(ReadType readType, long id);
    }
}
