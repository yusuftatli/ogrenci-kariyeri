using Microsoft.AspNetCore.Http;
using SCA.Common.Result;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class PictureManager : IPictureManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PictureManager(IHttpContextAccessor httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
        }

        public Task<ServiceResult> SaveImage(string ImgStr, string ImgName)
        {
            return null;
        }


    }
}
