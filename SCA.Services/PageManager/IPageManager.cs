using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IPageManager
    {
        Task<ServiceResult> GetAllBasicPages();
        Task<List<BasicPagesDto>> GetAllBasicPageForUI();
        Task<ServiceResult> CreateBasicPage(BasicPagesDto dto);
        Task<ServiceResult> UpdateState(long id, bool state);
    }
}
