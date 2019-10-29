using SCA.BLLServices.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.BLLServices
{
    public interface ICategoryService<U> : IGenericService<U> where U : class
    {
    }
}
