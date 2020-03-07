using Cake.Core.IO;
using Dapper;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using MySql.Data.MySqlClient;
using SCA.Common;
using SCA.Common.Base;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class UserManager : BaseClass, IUserManager
    {
        private ISender _sender;
        private IPictureManager _pictureManager;
        private IUserValidation _userValidation;
        private IHostingEnvironment _env;
        private IAuthManager _authManager;
        private readonly IErrorManagement _errorManagement;
        private readonly ISocialMediaManager _socialMedia;
        private readonly IRoleManager _roleManager;
        private readonly IDbConnection _db = new MySqlConnection(ConnectionString1);

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

        public async Task<ServiceResult> PasswordRenew(string emailAddress)
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
                    res = Result.ReturnAsFail(message: "Sistemde kayıtlı olmayan bir email adresi girdiniz");
                    return res;
                }

                string _pas = RandomPassword();
                string emailValue = await createEmailBody(_pas);

                string query = "update Users set Password = @Password  where EmailAddress = @emailAddress";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Password", MD5Hash(_pas));
                filter.Add("emailAddress", emailAddress);

                var data = _db.Execute(query, filter);



                //     ApiKey = "YOUR-API-KEY";

                //var task = SendEmail("Hello World from Elastic Email!", "fromAddress@exmple.com", "John Tester", new string[] { "toAddress@exmple.com" },
                //                    "<h1>Hello! This mail was sent by Elastic Email service.<h1>", "Hello! This mail was sent by Elastic Email service.");

                //task.ContinueWith(t =>
                //{
                //    if (t.Result == null)
                //        Console.WriteLine("Something went wrong. Check the logs.");
                //    else
                //    {
                //        Console.WriteLine("MsgID to store locally: " + t.Result.MessageID); // Available only if sent to a single recipient
                //        Console.WriteLine("TransactionID to store locally: " + t.Result.TransactionID);
                //    }
                //});

                //task.Wait();




                res = Result.ReturnAsSuccess(message: "yenileme şifreniz başarıyla email adresinize gönderilmiştir.");
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "CreateSector", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Şifre yenileme sırasında hata meydana geldi.");
            }
            return res;
        }

        //public async static Task<ElasticEmailClient.ApiTypes.EmailSend> SendEmail(string subject, string fromEmail, string fromName, string[] msgTo, string html, string text)
        //{
        //    try
        //    {
        //        return await ElasticEmailClient.Api.Email.SendAsync(subject, fromEmail, fromName, msgTo: msgTo, bodyHtml: html, bodyText: text);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is ApplicationException)
        //            Console.WriteLine("Server didn't accept the request: " + ex.Message);
        //        else
        //            Console.WriteLine("Something unexpected happened: " + ex.Message);

        //        return null;
        //    }
        //}

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
        //public async Task<ServiceResult> UpdateUserCategory(long userId, string category)
        //{
        //    ServiceResult res = new ServiceResult();
        //    try
        //    {
        //        string query = "Update Users set Category=@Category where Id = @userId";
        //        DynamicParameters filter = new DynamicParameters();
        //        filter.Add("userId", userId);
        //        filter.Add("Category", category);

        //        var result = await _db.ExecuteAsync(query, filter);
        //        res = Result.ReturnAsSuccess(message: "İlgi alanları güncelleme işlemi başarılı");
        //    }
        //    catch (Exception ex)
        //    {
        //        res = Result.ReturnAsFail(message: "İlgi alanları güncelleme işlemi sırasında hata meydana geldi.");
        //        await _errorManagement.SaveError(ex, userId, "CreateSector", PlatformType.Web);
        //    }
        //    return res;
        //}

        public async Task<ServiceResult> UpdateUserPassword(string oldPassword, string newPassword, string token)
        {
            ServiceResult res = new ServiceResult();
            long userId = JwtToken.GetUserId(token);
            string query = string.Empty;
            try
            {
                query = "select * from Users where Password = @Password and Id = @Id";
                DynamicParameters filter = new DynamicParameters();
                string md5 = MD5Hash(oldPassword);
                filter.Add("Password", md5);
                filter.Add("Id", userId);

                var result = await _db.QueryFirstOrDefaultAsync<UsersDTO>(query, filter);

                if (result == null)
                {
                    res = Result.ReturnAsSuccess(message: "Güncel şifrenizi hatalı girdiniz.");
                    return res;
                }
                else
                {
                    query = "update Users set Password = @Password where Id = @Id";
                    filter.Add("Id", userId);
                    filter.Add("Password", MD5Hash(newPassword));
                    await _db.ExecuteAsync(query, filter);
                }


                res = Result.ReturnAsSuccess(message: "Şifre güncelleme işlemi başarıyla tamamlandı");
            }
            catch (Exception ex)
            {
                res = Result.ReturnAsFail(message: "İlgi alanları güncelleme işlemi sırasında hata meydana geldi.");
                await _errorManagement.SaveError(ex, userId, "CreateSector", PlatformType.Web);
            }
            return res;
        }


        public async Task<ServiceResult> UpdateUserCategoryByMobil(string category, string token)
        {
            ServiceResult res = new ServiceResult();
            long userId = JwtToken.GetUserId(token);
            try
            {

                string query = "Update Users set Category=@Category where Id = @userId";
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

        public async Task<ServiceResult> CreateUserContact(UserContactDto dto)
        {
            ServiceResult res = new ServiceResult();
            try
            {

                string query = @"insert into UsersContact (Name, SurName, PhoneNumber, Email, Message, Datetime) 
                                 values (@Name, @SurName, @PhoneNumber, @Email, @Message, @Datetime);";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Name", dto.Name);
                filter.Add("SurName", dto.SurName);
                filter.Add("PhoneNumber", dto.PhoneNumber);
                filter.Add("Email", dto.Email);
                filter.Add("Message", dto.Message);
                filter.Add("Datetime", DateTime.Now);
                await _db.ExecuteAsync(query, filter);

                res = Result.ReturnAsSuccess(message: "Mesajınız başarılı bir şekilde kayıt edilmiştir.");

            }
            catch (Exception)
            {

            }
            return res;
        }

        public async Task<ServiceResult> CreateUserByMobil(UserMobilDto dto)
        {
            ServiceResult res = new ServiceResult();
            res = Result.ReturnAsSuccess();
            if (dto.Equals(null))
            {
                res = Result.ReturnAsFail(message: AlertResource.NoChanges);
            }
            if (dto.Id == 0)
            {
                if (await _userValidation.UserDataControl(dto.EmailAddress) == true)
                {
                    res = Result.ReturnAsFail(message: "Email adresi zaten kayıtlı");
                }
            }

            if (res.ResultCode != HttpStatusCode.OK)
            {
                return res;
            }

            try
            {
                string query = @"";
                string resultMessage = string.Empty;
                DynamicParameters filter = new DynamicParameters();
                if (dto.Id == 0)
                {
                    query = @"Insert Into Users (Id, Name, Surname, EmailAddress, PhoneNumber, Password, RoleTypeId, GenderId," +
                       "EducationStatusId, HighSchoolTypeId, UniversityId, ClassId, DepartmentId, NewGraduatedYear, Biography, CityId, IsActive," +
                       "ReferanceCode, BirthDate, IsStudent, HigSchoolName, MasterId, MasterDepartment, MasterGraduated, CreatedDate) values (@Id, @Name, @Surname, @EmailAddress, @PhoneNumber, @Password, @RoleTypeId, @GenderId," +
                       "@EducationStatusId, @HighSchoolTypeId, @UniversityId, @ClassId, @DepartmentId, @NewGraduatedYear, @Biography, @CityId, @IsActive," +
                       "@ReferanceCode, @BirthDate, 1, @HigSchoolName, @MasterId, @MasterDepartment, @MasterGraduated, @CreatedDate); SELECT LAST_INSERT_ID();";
                    resultMessage = "Kayıt işlemi başarıyla tamamlanmıştır.";
                    long uId = GetUserId();
                    filter.Add("Id", uId);
                    filter.Add("CreatedDate", DateTime.Now);
                    filter.Add("Password", MD5Hash(dto.Password));
                }
                else
                {
                    query = @"Update Users set Name = @Name, Surname = @Surname, EmailAddress = @EmailAddress, PhoneNumber = @PhoneNumber,
                              RoleTypeId = @RoleTypeId, GenderId = @GenderId, EducationStatusId = @EducationStatusId, HighSchoolTypeId = @HighSchoolTypeId,
                              UniversityId =@UniversityId, ClassId = @ClassId, DepartmentId = @DepartmentId, NewGraduatedYear = @NewGraduatedYear, 
                              Biography = @Biography, CityId = @CityId, IsActive = @IsActive, ReferanceCode = @ReferanceCode, BirthDate = @BirthDate,
                              UpdatedDate = @UpdatedDate, HigSchoolName = @HigSchoolName, MasterId = @MasterId, MasterDepartment = @MasterDepartment, 
                              MasterGraduated = @MasterGraduated
                              where Id = @Id";
                    resultMessage = "Güncelleme işlemi başarıyla tamamlanmıştır.";
                    filter.Add("Id", dto.Id);
                    filter.Add("UpdatedDate", DateTime.Now);
                }


                filter.Add("Name", dto.Name);
                filter.Add("Surname", dto.Surname);
                filter.Add("EmailAddress", dto.EmailAddress.ToLower());
                filter.Add("PhoneNumber", dto.PhoneNumber);

                filter.Add("RoleTypeId", 3);
                filter.Add("GenderId", (dto.GenderId == 1 && dto.GenderId == 2) ? dto.GenderId : 0);
                filter.Add("EducationStatusId", dto.EducationStatusId);
                filter.Add("HighSchoolTypeId", dto.HighSchoolTypeId);
                filter.Add("UniversityId", dto.UniversityId);
                filter.Add("DepartmentId", dto.DepartmentId);
                filter.Add("ClassId", dto.ClassId);
                filter.Add("NewGraduatedYear", dto.NewGraduatedYear);
                filter.Add("Biography", dto.Biography);
                filter.Add("CityId", dto.CityId);
                filter.Add("IsActive", 1);
                filter.Add("ReferanceCode", dto.ReferanceCode);
                filter.Add("BirthDate", (dto.BirthDate == null) ? DateTime.Now : dto.BirthDate);
                filter.Add("HigSchoolName", dto.HigSchoolName);
                filter.Add("MasterId", dto.MasterId);
                filter.Add("MasterDepartment", dto.MasterDepartment);
                filter.Add("MasterGraduated", dto.MasterGraduated);

                var resUser = await _db.ExecuteAsync(query, filter);

                if (dto.Id == 0)
                {

                    MobilUserLoginDto loginData = new MobilUserLoginDto();
                    loginData.username = dto.EmailAddress;
                    loginData.password = dto.Password;
                    loginData.push = dto.push;
                    loginData.UserRegisterPlatformId = dto.UserRegisterPlatformId;

                    res = await UserLoginByMobil(loginData);
                }
                else
                {
                    res = Result.ReturnAsSuccess(message: resultMessage);
                }

            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "CreateUserByMobil", PlatformType.Mobil);
                res = Result.ReturnAsFail(message: "Üye kaydı yapılırken hata meydana geldi.");
            }
            return res;
        }

        public async Task<List<CategoryPostDto>> GetUserCategory(long userId)
        {
            List<CategoryPostDto> res = new List<CategoryPostDto>();
            try
            {
                string query = string.Empty;
                query = "select * from UserCategories where UserId = @UserId";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("UserId", userId);

                var dataList = await _db.QueryAsync<CategoryPostDto>(query, filter) as List<CategoryPostDto>;
                res = dataList;

            }
            catch (Exception)
            {

            }
            return res;
        }

        public async Task<ServiceResult> UpdateUserCategory(string categories, string token)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                if (string.IsNullOrEmpty(categories))
                {
                    res = Result.ReturnAsFail(message: "Category listesi boş geçilemez");
                    return res;
                }

                string query = @"";
                long userId = JwtToken.GetUserId(token);
                DynamicParameters filter = new DynamicParameters();
                string resultMessage = string.Empty;
                string[] catlist = categories.Split(',');

                query = @"delete from UserCategories where UserId = @UserId";
                filter.Add("UserId", userId);
                await _db.ExecuteAsync(query, filter);

                foreach (string item in catlist)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        query = @"insert into UserCategories(UserId, CategoryId) values(@UserId, @CategoryId);";
                        filter.Add("UserId", userId);
                        filter.Add("CategoryId", Convert.ToInt64(item));
                        await _db.ExecuteAsync(query, filter);
                    }
                }
                res = Result.ReturnAsSuccess("İlgi alanları başarılı bir şekilde güncellendi.");

            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, 0, "UpdateUserCategory", PlatformType.Mobil);
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

        public async Task<ServiceResult> GetUserProfileInfo(string token)
        {
            ServiceResult res = new ServiceResult();
            UserProfileDto resultModel = new UserProfileDto();
            try
            {
                long userId = JwtToken.GetUserId(token);
                string query = string.Empty;
                query = @"select Id, Name, Surname, EmailAddress, PhoneNumber, ImagePath, GenderId, EducationStatusId, HighSchoolTypeId, UniversityId, 
                         push, DepartmentId, ClassId, Biography, CityId, concat('OK',Id) as ReferanceCode, BirthDate, HigSchoolName, MasterId, MasterDepartment, MasterGraduated, NewGraduatedYear from Users where Id = @Id";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("Id", userId);

                resultModel.ProfileInfo = await _db.QueryFirstOrDefaultAsync<UserProfileMobilDto>(query, filter);
                resultModel.Categories = await GetUserCategory(userId);
                res = Result.ReturnAsSuccess(data: resultModel);
            }
            catch (Exception ex)
            {

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
                string query = "select * from Users where EmailAddress = @EmailAddress and Password = @Password";
                DynamicParameters filter = new DynamicParameters();
                filter.Add("EmailAddress", dto.username.ToLower());
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

        public async Task<ServiceResult> UpdateUserPush(MobilUserLoginDto dto, string token)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                DynamicParameters filter = new DynamicParameters();
                string query = "";

                long userId = JwtToken.GetUserId(token);
                query = "update Users set push = @push, UserRegisterPlatformId = @UserRegisterPlatformId where Id = @userId";
                filter.Add("userId", userId);

                long platfomrId = 1;
                if (dto.UserRegisterPlatformId == "iOS")
                {
                    platfomrId = 2;
                }


                filter.Add("UserRegisterPlatformId", platfomrId);
                filter.Add("push", dto.push);

                await _db.ExecuteAsync(query, filter);
                res = Result.ReturnAsSuccess(data: true);
            }
            catch (Exception ex)
            {
                res = Result.ReturnAsFail(data: false);
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
            //filter.Add("Name", dto.Name);
            //filter.Add("Surname", dto.Surname);
            //filter.Add("EmailAddress", dto.EmailAddress);
            //filter.Add("PhoneNumber", dto.IsPhoneSend);
            //filter.Add("Password", dto.Password);
            //filter.Add("ImagePath", imagePath);
            //filter.Add("RoleTypeId", "3");
            //filter.Add("RoleExpiresDate", DateTime.Now.AddYears(50));
            //filter.Add("GenderId", dto.GenderId);
            //filter.Add("EducationStatusId", dto.EducationStatusId);
            //filter.Add("HighSchoolTypeId", dto.HighSchoolTypeId);
            //filter.Add("UniversityId", dto.UniversityId);
            //filter.Add("FacultyId", dto.FacultyId);
            //filter.Add("DepartmentId", dto.DepartmentId);
            //filter.Add("ClassId", dto.ClassId);
            //filter.Add("IsStudent", 1);
            //filter.Add("Biography", dto.Biography);
            //filter.Add("CityId", dto.CityId);
            //filter.Add("IsActive", dto.IsActive);
            //filter.Add("ReferanceCode", dto.ReferanceCode);
            //filter.Add("EnrollPlatformTypeId", 2);
            //filter.Add("BirthDate", dto.BirthDate);

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

        public async Task<ServiceResult> SaveUserPicture(string base64Data)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string[] splitData = base64Data.Split('/');
                Image data = Base64ToImage(splitData[1]);

                string fdf = "we";

            }
            catch (Exception ex)
            {

            }
            return res;
        }

        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);

            return image;
        }

        public async Task<bool> SavePicture(string value)
        {
            string baseData = @"data:image/webp;base64,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";

            Chilkat.BinData bd = new Chilkat.BinData();
            bd.AppendEncoded(baseData, "base64");

            //  Save to a GIF file.
            bool success = bd.WriteFile("/UserPicture");

            return true;
        }

    }
}
