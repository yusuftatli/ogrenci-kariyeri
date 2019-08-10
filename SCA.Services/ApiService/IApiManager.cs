using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IApiManager
    {
        Task<string> Get(string baseUrl);
    }
}
