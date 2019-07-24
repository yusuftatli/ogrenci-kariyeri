using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.SidebarComponents.NewsletterForm
{
    public class NewsletterFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("_NewsletterForm");
        }
    }
}
