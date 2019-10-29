using Microsoft.AspNetCore.Mvc;
using SCA.Entity.SPModels.SPResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.NewsWith3Columns
{
    public class NewsWith3ColumnsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<GetCategoryContents> model)
        {
            return View("_NewsWith3Columns", model);
        }
    }
}
