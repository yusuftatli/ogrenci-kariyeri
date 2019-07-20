using SCA.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IPictureManager
    {
        string SaveImage(string ImgStr, string ImgName);
    }
}
