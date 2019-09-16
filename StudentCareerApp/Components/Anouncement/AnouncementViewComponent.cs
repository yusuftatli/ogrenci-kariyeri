using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.Anouncement
{
    public class AnouncementViewComponent : ViewComponent
    {
        private readonly ICompanyClubManager _companyManager;

        public AnouncementViewComponent(ICompanyClubManager companyManager)
        {
            _companyManager = companyManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string seoUrl, List<AnouncementDto> model = null)
        {
            var returnModel = await _companyManager.GetCompanyAnnouncements(seoUrl);
            return View("_Anouncement", returnModel.Data as List<AnouncementDto>);
        }
        
    }
}
