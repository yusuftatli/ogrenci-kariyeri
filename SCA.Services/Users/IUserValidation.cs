using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IUserValidation
    {
        ServiceResult UserLoginValidation(LoginDto dto);
        ServiceResult UserDataValidation(UsersDTO dto);
        ServiceResult CreateUserValidation(UsersDTO dto);
        ServiceResult UserRegisterValidation(UserRegisterDto dto);
        Task<ServiceResult> ValidateCreateUserByWeb(UserWeblDto dto);
        Task<bool> UserDataControl(string emailAddress);
    }
}
