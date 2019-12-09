using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.UserComponents.ForgetPassword
{
    public class ForgetPasswordViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("_ForgetPassword");
        }
    }
}
