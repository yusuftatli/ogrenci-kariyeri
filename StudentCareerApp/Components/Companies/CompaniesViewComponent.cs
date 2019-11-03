using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Entity.Entities;
using SCA.Entity.Model;
using SCA.Entity.Model.CompanyClub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.Companies
{
    public class CompaniesViewComponent : ViewComponent
    {
        private readonly ICompanyClubService<SCA.Entity.Entities.CompanyClubs> _companyService;

        public CompaniesViewComponent(ICompanyClubService<SCA.Entity.Entities.CompanyClubs> companyService)
        {
            _companyService = companyService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var res = await _companyService.GetByWhereParams<CompanyComponent>(x => x.DeletedDate == null && x.CompanyClupType == SCA.Entity.Enums.CompanyClupType.Company);
            return View("_Company", res.Data as List<CompanyComponent>);
        }
    }
}
