using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services.Interface
{
    public interface IUserManager
    {
        Task<ServiceResult> CreateUser(UsersDTO dto);
        Task<ServiceResult> UpdateUser(UsersDTO dto);
        Task<ServiceResult> DeleteUser(long userId);
        Task UserLog(long userId);
    }
}
