using SCA.DapperRepository.Generic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.DapperRepository
{
    public interface IComment<U> : IGenericRepository<U> where U : class
    {
    }
}
