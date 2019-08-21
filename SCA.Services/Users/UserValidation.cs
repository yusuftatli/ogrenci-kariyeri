using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.DTO.ErrorDb;
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
            List<ErrorList> _er = new List<ErrorList>();

            if (string.IsNullOrEmpty(dto.username))
            {
               _er.Add(new ErrorList { Error = "Email adresi zaten kayıtlı" });
            }

            if (string.IsNullOrEmpty(dto.password))
            {
                _er.Add(new ErrorList { Error = "Kullanıcı adı zaten kayıtlı" });
            }

            if (_er.Count > 0)
            {
                _res = Result.ReturnAsFail(null, _er);
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

        public ServiceResult CreateUserValidation(UsersDTO dto)
        {
            ServiceResult _res = Result.ReturnAsSuccess();
            List<ErrorList> _er = new List<ErrorList>();
            _res = Result.ReturnAsSuccess();

            if (string.IsNullOrEmpty( dto.Name))
            {
                _er.Add(new ErrorList { Error = "İsim boş geçiemez" });
            }

            if (string.IsNullOrEmpty(dto.Surname))
            {
                _er.Add(new ErrorList { Error = "Soy boş geçiemez" });
            }

            if (string.IsNullOrEmpty(dto.EmailAddress))
            {
                _er.Add(new ErrorList { Error = "Email boş geçiemez" });
            }

            if (_userRepo.Any(x => x.EmailAddress == dto.EmailAddress))
            {
                _er.Add(new ErrorList { Error = "Email adresi zaten kayıtlı" });
            }
            if (_userRepo.Any(x => x.UserName == dto.UserName))
            {
                _er.Add(new ErrorList { Error = "Kullanıcı adı zaten kayıtlı" });
            }

            if (_er.Count > 0)
            {
                _res = Result.ReturnAsFail(null, _er);
            }

            return _res;
        }

        public ServiceResult UserRegisterValidation(UserRegisterDto dto)
        {
            ServiceResult _res = Result.ReturnAsSuccess();
            List<ErrorList> _er = new List<ErrorList>();
            _res = Result.ReturnAsSuccess();

            if (string.IsNullOrEmpty(dto.Name))
            {
                _er.Add(new ErrorList { Error = "İsim boş geçiemez" });
            }

            if (string.IsNullOrEmpty(dto.Surname))
            {
                _er.Add(new ErrorList { Error = "Soy boş geçiemez" });
            }

            if (string.IsNullOrEmpty(dto.EmailAddress))
            {
                _er.Add(new ErrorList { Error = "Email boş geçiemez" });
            }

            if (_userRepo.Any(x => x.UserName.Equals(dto.UserName)))
            {
                _er.Add(new ErrorList { Error = "Bu kullanıcı adı daha önce alınmış." });
            }

            if (_userRepo.Any(x=>x.EmailAddress.Equals(dto.EmailAddress)))
            {
                _er.Add(new ErrorList { Error = "Email adresi zaten kayıtlı" });
            }

            if (_er.Count > 0)
            {
                _res = Result.ReturnAsFail(null, _er);
            }

            return _res;
        }

    }
}
