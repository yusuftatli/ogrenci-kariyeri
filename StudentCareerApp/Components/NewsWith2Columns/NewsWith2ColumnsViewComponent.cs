using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.NewsWith2Columns
{
    public class NewsWith2ColumnsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<ContentForHomePageDTO> model)
        {
            return View("_NewsWith2Columns", model);
        }
    }
}
