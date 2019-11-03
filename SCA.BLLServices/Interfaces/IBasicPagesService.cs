using SCA.BLLServices.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.BLLServices
{
    public interface IBasicPagesService<U> : IGenericService<U> where U : class
    {
    }
}
