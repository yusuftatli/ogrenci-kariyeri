using SCA.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services.Interface
{
    public interface IPictureManager
    {
        Task<ServiceResult> SaveImage(string ImgStr, string ImgName);
    }
}
