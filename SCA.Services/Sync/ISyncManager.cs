using SCA.Common.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
  public  interface ISyncManager
    {
        Task<ServiceResult> SyncAssay();
    }
}
