using SCA.BLLServices.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.BLLServices
{
    public interface IContentService<U> : IGenericService<U> where U : class
    {
    }
}
