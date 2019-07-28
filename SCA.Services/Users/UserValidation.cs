using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class UserValidation : IUserValidation
    {
        private IGenericRepository<Users> _userRepo;
        private readonly IUnitofWork _unitOfWork;
        public UserValidation(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<Users>();
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

            if (dto.IsActive == true)
            {
                _res = Result.ReturnAsFail(message: "Sisteme Girişiniz Yetkiniz Bulunmamaktadır.", null);
            }

            return _res;
        }

    }
}
