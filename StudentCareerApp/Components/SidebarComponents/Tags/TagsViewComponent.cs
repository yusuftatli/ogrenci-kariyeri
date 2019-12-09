using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.SidebarComponents.Tags
{
    public class TagsViewComponent : ViewComponent
    {
        private readonly ITagManager _tagManager;

        public TagsViewComponent(ITagManager tagManager)
        {
            _tagManager = tagManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("_Tags",await _tagManager.GetTagsForUI());
        }
    }
}
