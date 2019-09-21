using AutoMapper;
using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class UserManager : IUserManager
    {
        private readonly IMapper _mapper;
        private ISender _sender;
        private readonly IUnitofWork _unitOfWork;
        private IGenericRepository<Users> _userRepo;
        private IGenericRepository<UserLog> _userLogRepo;
        private IGenericRepository<SocialMedia> _socialMediaRepo;
        private IPictureManager _pictureManager;
        private IUserValidation _userValidation;
        private IAuthManager _authManager;
        private readonly IErrorManagement _errorManagement;
        private readonly IRoleManager _roleManager;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public UserManager(IUnitofWork unitOfWork, IRoleManager roleManager, IMapper mapper, ISender sender, IPictureManager pictureManager, IErrorManagement errorManagement, IUserValidation userValidation, IAuthManager authManager)
        {
            _mapper = mapper;
            _sender = sender;
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<Users>();
            _userLogRepo = _unitOfWork.GetRepository<UserLog>();
            _socialMediaRepo = _unitOfWork.GetRepository<SocialMedia>();
            _errorManagement = errorManagement;
            _pictureManager = pictureManager;
            _userValidation = userValidation;
            _authManager = authManager;
            _roleManager = roleManager;
        }

        public async Task<ServiceResult> Dashboard(UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                List<ContentDashboardDto> data = new List<ContentDashboardDto>();
                ContentDashboardDto d = new ContentDashboardDto()
                {
                    Description = "Toplam Kullanıcı",
                    Count = 0
                };

                data.Add(d);
                string query = "SELECT r.Description ,count(u.Id) as Count FROM Users u inner join RoleType r on u.RoleTypeId=r.Id where r.Id <> 1 GROUP BY RoleTypeId";

                var result = await _db.QueryAsync<ContentDashboardDto>(query);
                data.AddRange(result);
                _res = Result.ReturnAsSuccess(data: data);
            }
            catch (Exception ex)
            {
                _res = _res = Result.ReturnAsFail(message: "Kullanıcı dashboard bilgisi yüklenemedi");
                await _errorManagement.SaveError(ex.ToString(), session.Id, "User/Dashboard", PlatformType.Web);
            }
            return _res;
        }
        public async Task<ServiceResult> UpdateUserCategory(long userId, string category)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "Update Users set Category=@Category where Id=@userId";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("userId", userId);
                filter.Add("Category", category);

                var result = await _db.ExecuteAsync(query, filter);
                _res = Result.ReturnAsSuccess(message: "İlgi alanları güncelleme işlemi başarılı");
            }
            catch (Exception ex)
            {
                _res = Result.ReturnAsFail(message: "İlgi alanları güncelleme işlemi sırasında hata meydana geldi.");
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }
        public async Task<ServiceResult> CreateUserByMobil(UserMobilDto dto)
        {
            ServiceResult _res = new ServiceResult();
            _res = Result.ReturnAsSuccess();
            if (dto.Equals(null))
            {
                _res = Result.ReturnAsFail(message: AlertResource.NoChanges);
            }
            if (await UserDataControl(dto.EmailAddress) == true)
            {
                _res = Result.ReturnAsFail(message: "Email adresi zaten kayıtlı");
            }

            if (_res.ResultCode!=HttpStatusCode.OK)
            {
                return _res;
            }

            try
            {
                string imagePath = "";

                string query = "Insert Into Users (Id,Name,Surname,EmailAddress,PhoneNumber,Password,ImagePath,Category,RoleTypeId,RoleExpiresDate,GenderId," +
                    "EducationStatusId,HighSchoolTypeId,UniversityId,FacultyId,DepartmentId,ClassId,IsStudent,Biography,CityId,IsActive," +
                    "ReferanceCode,EnrollPlatformTypeId,BirthDate) values (" +
                    "@Id,@Name,@Surname,@EmailAddress,@PhoneNumber,@Password,@ImagePath,@RoleTypeId,@Category,@RoleExpiresDate,@GenderId,@EducationStatusId," +
                    "@HighSchoolTypeId,@UniversityId,@FacultyId,@DepartmentId,@ClassId,@IsStudent,@Biography,@CityId,@IsActive,@ReferanceCode," +
                    "@EnrollPlatformTypeId,@BirthDate); SELECT LAST_INSERT_ID();";

                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", GetUserId());
                filter.Add("Name", dto.Name);
                filter.Add("Surname", dto.Surname);
                filter.Add("EmailAddress", dto.EmailAddress);
                filter.Add("PhoneNumber", dto.PhoneNumber);
                filter.Add("Password", dto.Password);
                filter.Add("Category", dto.Category);
                filter.Add("ImagePath", imagePath);
                filter.Add("RoleTypeId", "3");
                filter.Add("RoleExpiresDate", DateTime.Now.AddYears(50));
                filter.Add("GenderId", dto.GenderId);
                filter.Add("EducationStatusId", dto.EducationStatusId);
                filter.Add("HighSchoolTypeId", dto.HighSchoolTypeId);
                filter.Add("UniversityId", dto.UniversityId);
                filter.Add("FacultyId", dto.FacultyId);
                filter.Add("DepartmentId", dto.DepartmentId);
                filter.Add("ClassId", dto.ClassId);
                filter.Add("IsStudent", 1);
                filter.Add("Biography", dto.Biography);
                filter.Add("CityId", dto.CityId);
                filter.Add("IsActive", true);
                filter.Add("ReferanceCode", dto.ReferanceCode);
                filter.Add("EnrollPlatformTypeId", 1);
                filter.Add("BirthDate", dto.BirthDate);

                var userId = _db.Execute(query, filter);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), 0, "UserKayıt", PlatformType.Mobil);
                _res = Result.ReturnAsFail(message: "Kulanıcı kaydı yapılırken hata meydana geldi.");
            }
            return _res;
        }

        public long GetUserId()
        {
            string query = "select Id  from Users order by  Id desc limit 1";
            var _res = _db.Query<UsersDTO>(query).FirstOrDefault();
            return _res.Id+1;
        }

        public async Task<bool> UserDataControl(string emailAddress)
        {
            bool _res = false; ;
            string query = "select * from Users where Emailaddress=@Emailaddress";
            DynamicParameters filter = new DynamicParameters();
            filter.Add("Emailaddress", emailAddress);

            var result = await _db.QueryAsync<UsersDTO>(query, filter);

            if (result.Count() > 0)
            {
                _res = true;
            }
            else
            {
                _res = false;
            }
            return _res;
        }

        public async Task<List<UserModelList>> GetUserList()
        {
            List<UserModelList> _res = new List<UserModelList>();
            try
            {
                var listData = await _db.QueryAsync<UserModelList>("Users_ListAll", new { type = 1 }, commandType: CommandType.StoredProcedure) as List<UserModelList>;
                _res = listData;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        /// <summary>
        /// Kullanıcı bilgilerini döner
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<UsersDTO> GetUserInfo(long Id)
        {
            UsersDTO _res = new UsersDTO();
            try
            {
                string query = "select * from User where Id=@Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", Id);
                var data = await _db.QueryFirstAsync<UsersDTO>(query, filter);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        public async Task<ServiceResult> UserLoginByMobil(MobilUserLoginDto dto)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from Users where EmailAddress=@EmailAddress and Password=@Password";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("EmailAddress", dto.username);
                filter.Add("Password", MD5Hash(dto.password));

                var result = _db.Query<UserSession>(query, filter).FirstOrDefault();
                if (result != null)
                {
                    result.Token = _authManager.GenerateToken(result);
                    _res = Result.ReturnAsSuccess(message: "Hoşgeldin " + result.Name + "!", data: result);
                }
                else
                {
                    _res = Result.ReturnAsFail(message: "Kullanıcı adı veya şifre hatalı");
                }
            }
            catch (Exception ex)
            {
                _res = Result.ReturnAsFail(message: "Sisteme erişim sırasında hata meydana geldi");
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        public async Task<ServiceResult> CheckUserForLogin(string email, string password)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from Users where EmailAddress=@EmailAddress and Password=@Password";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("EmailAddress", email);
                filter.Add("Password", MD5Hash(password));

                var result = _db.Query<UserSession>(query, filter).FirstOrDefault();
                if (result != null)
                {
                    result.Token = _authManager.GenerateToken(result);
                    _res = Result.ReturnAsSuccess(message: "Hoşgeldin " + result.Name + "!", data: result);
                }
                else
                {
                    _res = Result.ReturnAsFail(message: "Kullanıcı adı veya şifre hatalı");
                }
            }
            catch (Exception ex)
            {
                _res = Result.ReturnAsFail(message: "Sisteme erişim sırasında hata meydana geldi");
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public async Task<ServiceResult> RegisterUser(UserRegisterDto dto)
        {
            if (dto.Equals(null))
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }

            ServiceResult _res = new ServiceResult();
            string resultMessage = "";

            _res = _userValidation.UserRegisterValidation(dto);
            if (HttpStatusCode.OK != _res.ResultCode)
            {
                return _res;
            }

            string imagePath = "";

            string query = "Insert Into Users (Name,Surname,EmailAddress,PhoneNumber,Password,ImagePath,RoleTypeId,RoleExpiresDate,GenderId," +
                "EducationStatusId,HighSchoolTypeId,UniversityId,FacultyId,DepartmentId,ClassId,IsStudent,Biography,CityId,IsActive," +
                "ReferanceCode,EnrollPlatformTypeId,BirthDate) values (" +
                "@Name,@Surname,@EmailAddress,@PhoneNumber,@Password,@ImagePath,@RoleTypeId,@RoleExpiresDate,@GenderId,@EducationStatusId," +
                "@HighSchoolTypeId,@UniversityId,@FacultyId,@DepartmentId,@ClassId,@IsStudent,@Biography,@CityId,@IsActive,@ReferanceCode" +
                "@EnrollPlatformTypeId,@BirthDate); SELECT LAST_INSERT_ID();";

            DynamicParameters filter = new DynamicParameters();
            filter.Add("Name", dto.Name);
            filter.Add("Surname", dto.Surname);
            filter.Add("EmailAddress", dto.EmailAddress);
            filter.Add("PhoneNumber", dto.IsPhoneSend);
            filter.Add("Password", dto.Password);
            filter.Add("ImagePath", imagePath);
            filter.Add("RoleTypeId", "3");
            filter.Add("RoleExpiresDate", DateTime.Now.AddYears(50));
            filter.Add("GenderId", dto.GenderId);
            filter.Add("EducationStatusId", dto.EducationStatusId);
            filter.Add("HighSchoolTypeId", dto.HighSchoolTypeId);
            filter.Add("UniversityId", dto.UniversityId);
            filter.Add("FacultyId", dto.FacultyId);
            filter.Add("DepartmentId", dto.DepartmentId);
            filter.Add("ClassId", dto.ClassId);
            filter.Add("IsStudent", 1);
            filter.Add("Biography", dto.Biography);
            filter.Add("CityId", dto.CityId);
            filter.Add("IsActive", dto.IsActive);
            filter.Add("ReferanceCode", dto.ReferanceCode);
            filter.Add("EnrollPlatformTypeId", 2);
            filter.Add("BirthDate", dto.BirthDate);

            var result = _db.ExecuteAsync(query, filter);

            return _res;
        }



        /// <summary>
        /// verilen idlere göre kullanıcıları listeler
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<UserShortInforDto> GetShortUserInfo(List<long> ids)
        {
            var listData = _mapper.Map<List<UserShortInforDto>>(_userRepo.GetAll(x => ids.Contains(x.Id)).ToList());
            return listData;
        }

        public async Task CreateUserLog(UserLogDto dto)
        {
            await Task.Run(() =>
            {
                _userLogRepo.Add(_mapper.Map<UserLog>(dto));
                _unitOfWork.SaveChanges();
            });

        }

        /// <summary>
        /// Kullanıcı kayıt (editör,admin vb)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServiceResult> CreateUser(UsersDTO dto)
        {
            if (dto.Equals(null))
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }


            ServiceResult _res = new ServiceResult();
            string resultMessage = "";

            _res = _userValidation.CreateUserValidation(dto);
            if (HttpStatusCode.OK != _res.ResultCode)
            {
                return _res;
            }

            if (dto.IsEmailSend)
            {
                await _sender.SendEmail();
            }

            if (dto.IsPhoneSend)
            {
                await _sender.SendMessage("");
            }

            if (!string.IsNullOrEmpty(dto.ImageData))
            {
                _pictureManager.SaveImage(dto.ImageData, dto.Name + "-" + dto.Surname);
            }

            Users _user = null;
            if (dto.Id == 0)
            {
                dto.IsActive = true;
                dto.BanCount = 0;
                dto.EnrollPlatformTypeId = PlatformType.Web;
                dto.HighSchoolTypeId = (dto.HighSchoolTypeId == 0) ? null : dto.HighSchoolTypeId;
                dto.UniversityId = (dto.UniversityId == 0) ? null : dto.UniversityId;
                dto.FacultyId = (dto.FacultyId == 0) ? null : dto.FacultyId;
                dto.DepartmentId = (dto.DepartmentId == 0) ? null : dto.DepartmentId;
                dto.CityId = (dto.CityId == 0) ? null : dto.CityId;
                dto.EducationStatusId = (dto.EducationStatusId == 0) ? null : dto.EducationStatusId;
                _user = _userRepo.Add(_mapper.Map<Users>(dto));
                resultMessage = "Kayıt İşlemi Başarılı";
            }
            else
            {
                _userRepo.Update(_mapper.Map<Users>(dto));
                resultMessage = "Güncelleme İşlemi Başarılı";
            }

            var result = _unitOfWork.SaveChanges();
            long _userId = _user.Id;

            if (dto.Id != 0)
            {
                var sData = _socialMediaRepo.GetAll(x => x.UserId == _userId).ToList();
                _socialMediaRepo.Delete(_mapper.Map<SocialMedia>(sData));
                _unitOfWork.SaveChanges();
            }

            List<SocialMediaDto> socialData = new List<SocialMediaDto>();
            socialData.Add(new SocialMediaDto { CompanyClupId = null, IsActive = true, SocialMediaType = SocialMediaType.Facebook, Url = dto.Facebook, UserId = _userId });
            socialData.Add(new SocialMediaDto { CompanyClupId = null, IsActive = true, SocialMediaType = SocialMediaType.Linkedin, Url = dto.Linkedin, UserId = _userId });
            socialData.Add(new SocialMediaDto { CompanyClupId = null, IsActive = true, SocialMediaType = SocialMediaType.Instagram, Url = dto.Instagram, UserId = _userId });

            _socialMediaRepo.AddRange(_mapper.Map<List<SocialMedia>>(socialData));
            _unitOfWork.SaveChanges();

            if (result.ResultCode != HttpStatusCode.OK)
            {
                await _errorManagement.SaveError(result.Message);
                _res = Result.ReturnAsFail(message: AlertResource.AnErrorOccurredWhenProcess, null);
            }
            else
            {
                _res = Result.ReturnAsSuccess(null, message: resultMessage, _userId);
            }

            return _res;
        }
        public async Task<ServiceResult> DeleteUser(long userId)
        {
            var deleteData = _userRepo.GetAll(x => x.Id == userId).FirstOrDefault();
            _userRepo.Delete(deleteData);
            var result = _unitOfWork.SaveChanges();
            return null;
        }

        public bool UserControl(string emailAddress)
        {
            bool _res = false;
            var userData = _userRepo.Get(x => x.EmailAddress == emailAddress);

            if (userData == null)
            {
                _res = true;
            }
            return _res;
        }

        /// <summary>
        /// üye durumunu Aktif/pasif yapar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateUserStatu(long id, bool value)
        {
            var data = _userRepo.Get(x => x.Id == id);
            data.IsActive = value;
            _userRepo.Update(_mapper.Map<Users>(data));
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(null, null, null);
        }

        public async Task<ServiceResult> UpdateUserRoleType(UserRoleTypeDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            string resultMessage = "";
            if (session.RoleTypeId == 1 || session.RoleTypeId == 2)
            {
                string query = "update Users set RoleTypeId = @RoleTypeId where Id = @Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("RoleTypeId", dto.RoleTypeId);
                filter.Add("Id", dto.userId);

                var data = _db.ExecuteAsync(query, filter);
                _res = Result.ReturnAsSuccess(message: resultMessage);
            }
            else
            {
                _errorManagement.SaveError("Bu işlemi yapmak için yetkiniz bulunmamaktadır.");
                _res = Result.ReturnAsFail(message: "Bu işlemi yapmak için yetkiniz bulunmamaktadır.");
            }
            return _res;
        }

    }
}
