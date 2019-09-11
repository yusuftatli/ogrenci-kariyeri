using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IErrorManagement
    {
        Task<string> SaveError(string error);
        Task<string> SaveError(string error, long userId, string process, PlatformType platformType);
    }
}
