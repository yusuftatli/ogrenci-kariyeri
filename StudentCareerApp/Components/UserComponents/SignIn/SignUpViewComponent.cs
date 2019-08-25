using Microsoft.AspNetCore.Mvc;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.UserComponents.SignIn
{
    public class SignUpViewComponent : ViewComponent
    {
        private readonly IDefinitionManager _definitionManager;
        private readonly IAddressManager _addressManager;

        public SignUpViewComponent(IDefinitionManager definitionManager, IAddressManager addressManager)
        {
            _definitionManager = definitionManager;
            _addressManager = addressManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(AllUniversityInformationDto model = null)
        {
            if (model == null)
            {
                model = new AllUniversityInformationDto
                {
                    Universities = await _definitionManager.GetUniversityForUI(),
                    Faculties = await _definitionManager.GetFacultyForUI(),
                    Departments = await _definitionManager.GetDepartmentForUI(),
                    Classes = await _definitionManager.GetStudentClassForUI(),
                    HighSchools = await _definitionManager.GetHighSchoolForUI(),
                    Cities = await _addressManager.CityList()
                };
            }
            return View("_SignUp", model);
        }
    }
}
