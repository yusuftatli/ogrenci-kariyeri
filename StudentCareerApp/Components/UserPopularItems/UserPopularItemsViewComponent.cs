using Microsoft.AspNetCore.Mvc;
using SCA.BLLServices;
using SCA.Entity.DTO;
using SCA.Entity.Entities;
using SCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentCareerApp.Components.UserPopularItems
{
    public class UserPopularItemsViewComponent : ViewComponent
    {
        private readonly IContentService<Content> _contentService;
        private readonly ICategoryManager _categoryManager;
        public UserPopularItemsViewComponent(IContentService<Content> contentService, ICategoryManager categoryManager)
        {
            _contentService = contentService;
            _categoryManager = categoryManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(long userId, int count)
        {
            List<ContentForHomePageDTO> contentList = new List<ContentForHomePageDTO>();
            var @params = new
            {
                userId,
                count
            };
            var res = await _contentService.SPQueryAsync<object, ContentForHomePageDTO>(@params, "GetUserContents");
            List<long> ids = new List<long>();
            foreach (ContentForHomePageDTO item in res.Data as List<ContentForHomePageDTO>)
            {
                ids.Add(item.Id);
            }
            List<CategoriesDto> categoryList = await _categoryManager.GetCategoryListById(ids);

            foreach (ContentForHomePageDTO item in res.Data as List<ContentForHomePageDTO>)
            {
                string value = string.Empty;
                var dataList = categoryList.Where(x => x.Id == item.Id).ToList();
                if (dataList != null)
                {
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        if (i + 1 == dataList.Count)
                        {
                            value += dataList[i].Description;
                        }
                        else
                        {
                            value += dataList[i].Description + ", ";
                        }
                    }
                }
                else
                {
                    item.Category = "";
                }
                item.Category = value;
            }
            return View("_UserPopularItems", res.Data as List<ContentForHomePageDTO>);
        }
    }
}
