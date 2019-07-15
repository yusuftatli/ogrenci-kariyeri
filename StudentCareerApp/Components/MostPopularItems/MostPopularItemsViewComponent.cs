using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using System.Collections.Generic;

namespace StudentCareerApp.Components.MostPopularItems
{
    public class MostPopularItemsViewComponent : ViewComponent
    {
        public MostPopularItemsViewComponent() { }

        public IViewComponentResult Invoke(List<ContentForHomePageDTO> model)
        {
            return View("_MostPopularItems", model);
        }
    }
}
