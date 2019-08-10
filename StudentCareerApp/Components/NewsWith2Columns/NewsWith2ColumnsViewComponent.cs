using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.NewsWith2Columns
{
    public class NewsWith2ColumnsViewComponent : ViewComponent
    {
        private readonly IContentManager _contentManager;

        public NewsWith2ColumnsViewComponent(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public IViewComponentResult Invoke(int count, List<ContentForHomePageDTO> model = null)
        {
            var res = model ?? await _contentManager.GetContentForHomePage()
            return View("_NewsWith2Columns", model);
        }
    }
}
