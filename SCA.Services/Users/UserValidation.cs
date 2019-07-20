using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class UserValidation : IUserValidation
    {
        public UserValidation()
        {

        }

        public ServiceResult UserLoginValidation(LoginDto dto)
        {
            ServiceResult _res = Result.ReturnAsSuccess();

            if (string.IsNullOrEmpty(dto.username))
            {
                _res = Result.ReturnAsFail(message: "Kullanıcı Adı Boş Geçilemez", null);
            }

            if (string.IsNullOrEmpty(dto.password))
            {
                _res = Result.ReturnAsFail(message: "Kullanıcı Şifre Boş Geçilemez", null);
            }

            return _res;
        }

        public ServiceResult UserDataValidation(UsersDTO dto)
        {
            ServiceResult _res = Result.ReturnAsSuccess();

            if (dto.IsActive == false)
            {
                _res = Result.ReturnAsFail(message: "Sisteme Girişiniz Yetkiniz Bulunmamaktadır.", null);
            }

            return _res;
        }

    }
}
