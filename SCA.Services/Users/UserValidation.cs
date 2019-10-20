using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.DTO.ErrorDb;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class UserValidation : IUserValidation
    {
        public UserValidation()
        {
        }
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public ServiceResult UserLoginValidation(LoginDto dto)
        {
            ServiceResult res = Result.ReturnAsSuccess();
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
                res = Result.ReturnAsFail(null, _er);
            }

            return res;
        }

        public async Task<ServiceResult> ValidateCreateUserByWeb(UserWeblDto dto)
        {
            ServiceResult res = new ServiceResult();
            res = Result.ReturnAsSuccess();

            if (await UserDataControl(dto.EmailAddress) == true)
            {
                res = Result.ReturnAsFail(message: "Email adresi zaten kayıtlı");
                return res;
            }

            if (dto.Equals(null))
            {
                res = Result.ReturnAsFail(message: AlertResource.NoChanges);
                return res;
            }

            if (string.IsNullOrEmpty(dto.Surname))
            {
                res = Result.ReturnAsFail(message: "Soyad boş geçilemez.");
                return res;
            }

            if (string.IsNullOrEmpty(dto.EmailAddress))
            {
                res = Result.ReturnAsFail(message: "Email Adresi boş geçilemez.");
                return res;
            }

            if (string.IsNullOrEmpty(dto.Password))
            {
                res = Result.ReturnAsFail(message: "Şifre boş geçilemez.");
                return res;
            }

            if (string.IsNullOrEmpty(dto.Name))
            {
                res = Result.ReturnAsFail(message: "Ad boş geçilemez.");
                return res;
            }
            if (dto.RoleTypeId==0)
            {
                res = Result.ReturnAsFail(message: "Role tipi boş geçilemez.");
                return res;
            }
            return res;
        }

        public ServiceResult UserDataValidation(UsersDTO dto)
        {
            ServiceResult res = Result.ReturnAsSuccess();

            if (dto.IsActive == true)
            {
                res = Result.ReturnAsFail(message: "Sisteme Girişiniz Yetkiniz Bulunmamaktadır.", null);
            }

            return res;
        }

        public ServiceResult CreateUserValidation(UsersDTO dto)
        {
            ServiceResult res = Result.ReturnAsSuccess();
            List<ErrorList> _er = new List<ErrorList>();
            res = Result.ReturnAsSuccess();

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

            //if (_userRepo.Any(x => x.EmailAddress == dto.EmailAddress))
            //{
            //    _er.Add(new ErrorList { Error = "Email adresi zaten kayıtlı" });
            //}
            //if (_userRepo.Any(x => x.UserName == dto.UserName))
            //{
            //    _er.Add(new ErrorList { Error = "Kullanıcı adı zaten kayıtlı" });
            //}

            if (_er.Count > 0)
            {
                res = Result.ReturnAsFail(null, _er);
            }

            return res;
        }

        public ServiceResult UserRegisterValidation(UserRegisterDto dto)
        {
            ServiceResult res = Result.ReturnAsSuccess();
            List<ErrorList> _er = new List<ErrorList>();
            res = Result.ReturnAsSuccess();

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

            //if (_userRepo.Any(x => x.UserName.Equals(dto.UserName)))
            //{
            //    _er.Add(new ErrorList { Error = "Bu kullanıcı adı daha önce alınmış." });
            //}

            //if (_userRepo.Any(x=>x.EmailAddress.Equals(dto.EmailAddress)))
            //{
            //    _er.Add(new ErrorList { Error = "Email adresi zaten kayıtlı" });
            //}

            if (_er.Count > 0)
            {
                res = Result.ReturnAsFail(null, _er);
            }

            return res;
        }

        public async Task<bool> UserDataControl(string emailAddress)
        {
            bool res = false; ;
            string query = "select * from Users where Emailaddress=@Emailaddress";
            DynamicParameters filter = new DynamicParameters();
            filter.Add("Emailaddress", emailAddress);

            var result = await _db.QueryAsync<UsersDTO>(query, filter);

            if (result.Count() > 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }

    }
}
