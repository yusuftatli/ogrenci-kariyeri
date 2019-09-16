using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.CompanyClubComponents.CompanyClubInformation
{
    public class CompanyClubInformationViewComponent : ViewComponent
    {
        private readonly ICompanyClubManager _companyManager;

        public CompanyClubInformationViewComponent(ICompanyClubManager companyManager)
        {
            _companyManager = companyManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string seoUrl, CompanyClubInformationDto model = null)
        {
            var res = await _companyManager.GetCompanyInformation(seoUrl);
            return View("_CompanyClubInformation", res.Data as CompanyClubInformationDto);
        }
    }
}
