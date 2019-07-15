using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services.Interface
{
    public interface IAuthManager
    {
        Task<ServiceResult> UserLogin(LoginModel dto);
        Task<ServiceResult> PasswordForget(string emailAddress);
        Task<ServiceResult> ReNewPassword(string guidValue);
    }
}
