using Microsoft.AspNetCore.Mvc;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.SidebarComponents.Tags
{
    public class TagsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = FakeData();
            return View("_Tags", model);
        }

        public List<TagDto> FakeData()
        {
            return new List<TagDto>
            {
                new TagDto
                {
                    Description = "Tag 1",
                    Id = 1,
                    Hit = 25
                },
                new TagDto
                {
                    Description = "Tag 2",
                    Id = 2,
                    Hit = 24
                },
                new TagDto
                {
                    Description = "Tag 3",
                    Id = 3,
                    Hit = 22
                },
                new TagDto
                {
                    Description = "Tag 4",
                    Id = 4,
                    Hit = 33
                },
                new TagDto
                {
                    Description = "Tag 5",
                    Id = 5,
                    Hit = 12
                },
                new TagDto
                {
                    Description = "Tag 6",
                    Id = 6,
                    Hit = 14
                },
                new TagDto
                {
                    Description = "Tag 7",
                    Id = 7,
                    Hit = 11
                }
            };
        }
    }
}
