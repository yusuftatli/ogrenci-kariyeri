using SCA.DapperRepository.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.DapperRepository
{
    public interface IBasicPages<U> : IGenericRepository<U> where U : class
    {
    }
}
