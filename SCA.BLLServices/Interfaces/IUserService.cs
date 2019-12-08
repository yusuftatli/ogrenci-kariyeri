using SCA.BLLServices.Generic;
using SCA.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.BLLServices
{
    public interface IUserService<U> : IGenericService<U> where U : class
    {
    }
}
