using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IMenuManager
    {
        Task<ServiceResult> CreateScreenMaster(ScreenMaster dto);
        Task<ServiceResult> ScreenMasterState(long id, bool state);
        Task<ServiceResult> CreateScreenDetail(ScreenDetailDto dto);
        Task<ServiceResult> ScreenDetailState(long id, bool state);
    }
}
