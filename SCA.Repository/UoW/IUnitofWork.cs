using SCA.Common.Result;
using SCA.Repository.Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SCA.Repository.UoW
{
    public interface IUnitofWork //: IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class, new();
        ServiceResult SaveChanges();
    }
}
