using Dapper;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using MySql.Data.MySqlClient;
using SCA.Common;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class UserManager : IUserManager
    {
        private ISender _sender;
        private IPictureManager _pictureManager;
        private IUserValidation _userValidation;
        private IHostingEnvironment _env;
        private IAuthManager _authManager;
        private readonly IErrorManagement _errorManagement;
        private readonly ISocialMediaManager _socialMedia;
        private readonly IRoleManager _roleManager;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public UserManager(IRoleManager roleManager,
            IHostingEnvironment env,
            ISender sender,
            IPictureManager pictureManager,
            IErrorManagement errorManagement,
            IUserValidation userValidation,
            IAuthManager authManager,
            ISocialMediaManager socialManager)
        {
            _sender = sender;
            _errorManagement = errorManagement;
            _env = env;
            _pictureManager = pictureManager;
            _userValidation = userValidation;
            _authManager = authManager;
            _roleManager = roleManager;
            _socialMedia = socialManager;
        }

        public async Task<ServiceResult> PasswordRenew(string emailAddress, string token)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                if (string.IsNullOrEmpty(emailAddress))
                {
                    res = Result.ReturnAsFail(message: "Email adresi boş olamaz");
                    return res;
                }

                if (await _userValidation.UserDataControl(emailAddress) == false)
                {
                    res = Result.ReturnAsFail(message: "Email Kayıtlı değil");
                    return res;
                }

                string _pas = RandomPassword();
                string emailValue = await createEmailBody(_pas);

                string query = "update Users set Password = @Password  where EmailAddress = 'yusufcantatli@hotmail.com'";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Password", MD5Hash(_pas));

                var data = _db.Execute(query, filter);
                long userId = 5677; //JwtToken.GetUserId(token);

                EmailSettings emailSetting = await _sender.GetEmailSetting("PASSRENEW");
                EmailsDto emailData = new EmailsDto
                {
                    Body = emailValue,
                    Subject = "Öğrenci Kariyer Şifre Yenileme",
                    ToEmail = emailAddress,
                    IsSend = false,
                    UserId = userId,
                    SendDate = DateTime.Now,
                    CcEmail = "",
                    FromEmail = emailSetting.UsernameEmail,
                    Process = "Şifre Yenileme"
                };

                await _sender.SaveEmails(emailData);
                res = Result.ReturnAsSuccess(message: "yenileme şifreniz başarıyla email adresinize gönderilmiştir.");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "CreateSector", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Şifre yenileme sırasında hata meydana geldi.");
            }
            return res;
        }

        private async Task<string> createEmailBody(string password)
        {
            string body = string.Empty;

            body = await _sender.GetEmailTemplate("PASSRENEW");
            body = body.Replace("{Password}", password);

            return body;
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public async Task<ServiceResult> Dashboard(UserSession session)
        {
            ServiceResult res = new ServiceResult();
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
                res = Result.ReturnAsSuccess(data: data);
            }
            catch (Exception ex)
            {
                res = res = Result.ReturnAsFail(message: "Kullanıcı dashboard bilgisi yüklenemedi");
                await _errorManagement.SaveError(ex, session.Id, "User/Dashboard", PlatformType.Web);
            }
            return res;
        }
        public async Task<ServiceResult> UpdateUserCategory(long userId, string category)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "Update Users set Category=@Category where Id=@userId";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("userId", userId);
                filter.Add("Category", category);

                var result = await _db.ExecuteAsync(query, filter);
                res = Result.ReturnAsSuccess(message: "İlgi alanları güncelleme işlemi başarılı");
            }
            catch (Exception ex)
            {
                res = Result.ReturnAsFail(message: "İlgi alanları güncelleme işlemi sırasında hata meydana geldi.");
                await _errorManagement.SaveError(ex, userId, "CreateSector", PlatformType.Web);
            }
            return res;
        }

        public async Task<ServiceResult> CreateUserByWeb(UserWeblDto dto, string token)
        {
            ServiceResult res = new ServiceResult();
            res = Result.ReturnAsSuccess();

            var errror = await _userValidation.ValidateCreateUserByWeb(dto);
            if (errror.ResultCode != HttpStatusCode.OK)
            {
                return errror;
            }

            try
            {
                string query = string.Empty;
                long _userId = JwtToken.GetUserId(token);
                long saveUserId = 0;
                DynamicParameters filter = new DynamicParameters();
                if (dto.Id == 0)
                {
                    query = "Insert Into Users (Id, Name, Surname, EmailAddress, PhoneNumber, Password, ImagePath, GenderId, RoleTypeId, " +
                   "BirthDate, RoleExpiresDate, Biography, EnrollPlatformTypeId, IsStudent, ReferanceCode, CreatedUserId, CreatedDate) " +
                   "values (@Id, @Name, @Surname, @EmailAddress, @PhoneNumber, @Password, @ImagePath, @GenderId, @RoleTypeId,BirthDate, " +
                   "@RoleExpiresDate, @Biography, @EnrollPlatformTypeId, @IsStudent, @ReferanceCode, @CreatedUserId, @CreatedDate); SELECT LAST_INSERT_ID();";
                    saveUserId = GetUserId();
                    filter.Add("Id", saveUserId);
                    filter.Add("CreatedUserId", _userId);
                    filter.Add("CreatedDate", DateTime.Now);
                }
                else
                {
                    query = "Update Users set  Name = @Name, Surname = @Surname, EmailAddress = @EmailAddress, PhoneNumber = @PhoneNumber, " +
                        "Password = @Password, ImagePath = @ImagePath, GenderId = @GenderId, RoleTypeId = @RoleTypeId, BirthDate = @BirthDate, " +
                        "RoleExpiresDate = @RoleExpiresDate, Biography = @Biography, EnrollPlatformTypeId = @EnrollPlatformTypeId, IsStudent = @IsStudent," +
                        " ReferanceCode = @ReferanceCode, UpdatedUserId = @UpdatedUserId, UpdatedDate = @UpdatedDate" +
                        "where Id = @Id";
                    filter.Add("Id", "Id");
                    filter.Add("UpdatedUserId", _userId);
                    filter.Add("UpdatedDate", DateTime.Now);
                }

                filter.Add("Name", dto.Name);
                filter.Add("Surname", dto.Surname);
                filter.Add("EmailAddress", dto.EmailAddress);
                filter.Add("PhoneNumber", dto.PhoneNumber);
                filter.Add("Password", MD5Hash(dto.Password));
                filter.Add("ImagePath", "");//SaveImage(dto.ImaageData));
                filter.Add("RoleTypeId", dto.RoleTypeId);
                filter.Add("RoleExpiresDate", dto.RoleExpiresDate);
                filter.Add("GenderId", dto.GenderId);
                filter.Add("IsStudent", 0);
                filter.Add("Biography", dto.Biography);
                filter.Add("IsActive", true);
                filter.Add("ReferanceCode", dto.ReferanceCode);
                filter.Add("EnrollPlatformTypeId", 2);
                filter.Add("BirthDate", dto.BirthDate);

                var Id = _db.Execute(query, filter);
                await _socialMedia.CreateSocialMedia(dto, _userId);
                res = Result.ReturnAsSuccess(message: "Kullancı kayıt işlemi başarılı");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "CreateUserByMobil", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Üye kaydı yapılırken hata meydana geldi.+ " + ex.Message);
            }
            return res;
        }

        public async Task<ServiceResult> UpdateUserByWeb(UserWeblDto dto)
        {
            ServiceResult res = new ServiceResult();
            res = Result.ReturnAsSuccess();

            var errror = await _userValidation.ValidateCreateUserByWeb(dto);
            if (errror.ResultCode != HttpStatusCode.OK)
            {
                return errror;
            }

            try
            {
                string query = string.Empty;

                query = "Update Users set  Name = @Name, Surname = @Surname, EmailAddress = @EmailAddress, PhoneNumber = @PhoneNumber, " +
                    "ImagePath = @ImagePath, GenderId = @GenderId,  BirthDate = @BirthDate, " +
                    "Biography = @Biography, UpdatedUserId = @UpdatedUserId, UpdatedDate = @UpdatedDate" +
                    "where Id = @Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", "Id");
                filter.Add("UpdatedUserId", dto.Id);
                filter.Add("UpdatedDate", DateTime.Now);
                filter.Add("Name", dto.Name);
                filter.Add("Surname", dto.Surname);
                filter.Add("EmailAddress", dto.EmailAddress);
                filter.Add("PhoneNumber", dto.PhoneNumber);
                filter.Add("ImagePath", "");//SaveImage(dto.ImaageData));
                filter.Add("RoleTypeId", dto.RoleTypeId);
                filter.Add("RoleExpiresDate", dto.RoleExpiresDate);
                filter.Add("GenderId", dto.GenderId);
                filter.Add("Biography", dto.Biography);
                filter.Add("BirthDate", dto.BirthDate);

                var Id = await _db.ExecuteAsync(query, filter);
                await _socialMedia.CreateSocialMedia(dto, dto.Id);
                res = Result.ReturnAsSuccess(message: "Bilgileri güncelleme işlemi başarılı");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "UpdateUserByWeb", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Üye kaydı yapılırken hata meydana geldi.+ " + ex.Message);
            }
            return res;
        }


        public async Task<string> SaveImage(string base64Value)
        {
            string path = string.Empty;
            return path;
        }

        public async Task<ServiceResult> CreateUserByMobil(UserMobilDto dto)
        {
            ServiceResult res = new ServiceResult();
            res = Result.ReturnAsSuccess();
            if (dto.Equals(null))
            {
                res = Result.ReturnAsFail(message: AlertResource.NoChanges);
            }
            if (await _userValidation.UserDataControl(dto.EmailAddress) == true)
            {
                res = Result.ReturnAsFail(message: "Email adresi zaten kayıtlı");
            }

            if (res.ResultCode != HttpStatusCode.OK)
            {
                return res;
            }

            try
            {
                string imagePath = "";

                string query = "Insert Into Users (Id,Name,Surname,EmailAddress,PhoneNumber,Password,ImagePath,Category,RoleTypeId,RoleExpiresDate,GenderId," +
                    "EducationStatusId,HighSchoolTypeId,UniversityId,FacultyId,DepartmentId,ClassId,IsStudent,Biography,CityId,IsActive," +
                    "ReferanceCode,EnrollPlatformTypeId,BirthDate, CreatedDate) values (" +
                    "@Id,@Name,@Surname,@EmailAddress,@PhoneNumber,@Password,@ImagePath,@RoleTypeId,@Category,@RoleExpiresDate,@GenderId,@EducationStatusId," +
                    "@HighSchoolTypeId,@UniversityId,@FacultyId,@DepartmentId,@ClassId,@IsStudent,@Biography,@CityId,@IsActive,@ReferanceCode," +
                    "@EnrollPlatformTypeId,@BirthDate, @CreatedDate); SELECT LAST_INSERT_ID();";

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
                filter.Add("CreatedDate", DateTime.Now);

                var userId = _db.Execute(query, filter);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "CreateUserByMobil", PlatformType.Mobil);
                res = Result.ReturnAsFail(message: "Üye kaydı yapılırken hata meydana geldi.");
            }
            return res;
        }

        public long GetUserId()
        {
            string query = "select Id  from Users order by  Id desc limit 1";
            var res = _db.Query<UsersDTO>(query).FirstOrDefault();
            return res.Id + 1;
        }



        public async Task<List<UserModelList>> GetUserList()
        {
            List<UserModelList> res = new List<UserModelList>();
            try
            {
                var listData = await _db.QueryAsync<UserModelList>("Users_ListAll", new { type = 1 }, commandType: CommandType.StoredProcedure) as List<UserModelList>;
                res = listData;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetUserList", PlatformType.Mobil);
            }
            return res;
        }

        /// <summary>
        /// Kullanıcı bilgilerini döner
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> GetUserInfo(long userId)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = @"select 
                                u.Id,
                                u.Name, 
                                u.Surname, 
                                u.EmailAddress, 
                                u.PhoneNumber, 
                                u.Password, 
                                u.Category, 
                                u.ImagePath, 
                                u.RoleTypeId, 
                                r.Description as RoleType,
                                u.RoleExpiresDate, 
                                case when u.GenderId = '1' then 'Bay' else 'Bayan' end as Gender, 
                                u.Biography, 
                                u.BirthDate
                                  from Users u
                                left join RoleType r on u.RoleTypeId = r.Id
                                where u.Id = @Id
                                order by Id desc limit 1 ";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", userId);
                var data = await _db.QueryFirstAsync<UsersDTO>(query, filter);
                data.socialList = await _socialMedia.GetSocialMedia(userId);
                List<SocialMediaDto> socialListData = await _socialMedia.GetSocialMedia(userId);
                if (socialListData.Count > 0)
                {
                    foreach (SocialMediaDto item in socialListData)
                    {
                        if (item.SocialMediaType == SocialMediaType.Facebook)
                        {
                            data.Facebook = item.Url;
                        }
                        if (item.SocialMediaType == SocialMediaType.Linkedin)
                        {
                            data.Linkedin = item.Url;
                        }
                        if (item.SocialMediaType == SocialMediaType.Instagram)
                        {
                            data.Instagram = item.Url;
                        }
                    }
                }
                _res = Result.ReturnAsSuccess(data: data);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetUserInfo", PlatformType.Mobil);
            }
            return _res;
        }

        public async Task<ServiceResult> UserLoginByMobil(MobilUserLoginDto dto)
        {
            ServiceResult res = new ServiceResult();
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
                    res = Result.ReturnAsSuccess(message: "Hoşgeldin " + result.Name + "!", data: result);
                }
                else
                {
                    res = Result.ReturnAsFail(message: "Kullanıcı adı veya şifre hatalı");
                }
            }
            catch (Exception ex)
            {
                res = Result.ReturnAsFail(message: "Sisteme erişim sırasında hata meydana geldi");
                await _errorManagement.SaveError(ex, null, "UserLoginByMobil", PlatformType.Mobil);
            }
            return res;
        }

        public async Task<ServiceResult> CheckUserForLogin(string email, string password)
        {
            ServiceResult res = new ServiceResult();
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
                    res = Result.ReturnAsSuccess(message: "Hoşgeldin " + result.Name + "!", data: result);
                }
                else
                {
                    res = Result.ReturnAsFail(message: "Kullanıcı adı veya şifre hatalı");
                }
            }
            catch (Exception ex)
            {
                res = Result.ReturnAsFail(message: "Sisteme erişim sırasında hata meydana geldi");
                await _errorManagement.SaveError(ex, null, "CheckUserForLogin", PlatformType.Mobil);
            }
            return res;
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

            ServiceResult res = new ServiceResult();
            string resultMessage = "";

            res = _userValidation.UserRegisterValidation(dto);
            if (HttpStatusCode.OK != res.ResultCode)
            {
                return res;
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

            return res;
        }



        /// <summary>
        /// verilen idlere göre kullanıcıları listeler
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<UserShortInforDto> GetShortUserInfo(List<long> ids)
        {
            //var listData = _mapper.Map<List<UserShortInforDto>>(_userRepo.GetAll(x => ids.Contains(x.Id)).ToList());
            return null;
        }

        public async Task CreateUserLog(UserLogDto dto)
        {


        }

        /// <summary>
        /// üye durumunu Aktif/pasif yapar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateUserStatu(long id, bool value)
        {

            return Result.ReturnAsSuccess(null, null, null);
        }

        public async Task<ServiceResult> UpdateUserRoleType(UserRoleTypeDto dto, UserSession session)
        {
            ServiceResult res = new ServiceResult();
            string resultMessage = "";
            if (session.RoleTypeId == 1 || session.RoleTypeId == 2)
            {
                string query = "update Users set RoleTypeId = @RoleTypeId where Id = @Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("RoleTypeId", dto.RoleTypeId);
                filter.Add("Id", dto.userId);

                var data = _db.ExecuteAsync(query, filter);
                res = Result.ReturnAsSuccess(message: "Rol atama işlemi başarılı");
            }
            else
            {
                res = Result.ReturnAsFail(message: "Bu işlemi yapmak için yetkiniz bulunmamaktadır.");
            }
            return res;
        }

    }
}
