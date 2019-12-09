using Microsoft.AspNetCore.Mvc;
using SCA.Common;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.CompanyClubHeaderViewComponent
{
    public class CompanyClubHeaderViewComponent : ViewComponent
    {

        private readonly ICompanyClubManager _companyManager;

        public CompanyClubHeaderViewComponent(ICompanyClubManager companyManager)
        {
            _companyManager = companyManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string seoUrl, CompClubHeaderDto model = null)
        {
            var res = await _companyManager.GetCompanyHeader(seoUrl, Convert.ToInt64(HttpContext.GetSessionData<UserSession>("userInfo")?.Id));
            return View("_CompanyClubHeader", res.Data);
        }
    }
}
