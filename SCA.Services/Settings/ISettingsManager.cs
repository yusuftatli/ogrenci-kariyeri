using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface ISettingsManager
    {
        Task<ServiceResult> GetSettingValue();
        Task<ServiceResult> SetSettingsValue(MultipleCountDto dto);
        Task<ServiceResult> GetContentMultipleCountOnly(long id);
        Task<ServiceResult> SetContentMultipleCountOnly(MultipleCountDto dto);
    }
}
