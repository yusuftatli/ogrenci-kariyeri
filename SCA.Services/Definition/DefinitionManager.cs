using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SCA.Common;
using SCA.Common.Base;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Model;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class DefinitionManager :BaseClass, IDefinitionManager
    {
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection(ConnectionString1);

        public DefinitionManager(IErrorManagement errorManagement)
        {
            _errorManagement = errorManagement;
        }

        #region Department
        public async Task<ServiceResult> GetDepartment(string token)
        {
            ServiceResult res = new ServiceResult();
            try
            {

                string query = "select Id, Description as DepartmentName from Departmnet";
                var result = await _db.QueryAsync<DepartmentDto>(query) as List<DepartmentDto>;
                res = Result.ReturnAsSuccess(data: result);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "GetDepartment", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Departman bilgisi yüklenirken hata meydana geldi");
            }
            return res;
        }
        public async Task<ServiceResult> GetDepartmentForUI1()
        {
            ServiceResult res = new ServiceResult();
            try
            {

                string query = "select Id, Description as DepartmentName from Departmnet";
                var result = await _db.QueryAsync<DepartmentDto>(query) as List<DepartmentDto>;
                res = Result.ReturnAsSuccess(data: result);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetDepartment", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Departman bilgisi yüklenirken hata meydana geldi");
            }
            return res;
        }

        public async Task<List<DepartmentDto>> GetDepartmentForUI()
        {
            List<DepartmentDto> res = new List<DepartmentDto>();
            try
            {
                string query = "select Id, Description as DepartmentName from Departmnet";
                res = await _db.QueryAsync<DepartmentDto>(query) as List<DepartmentDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetDepartmentForUI", PlatformType.Web);
            }
            return res;
        }

        public async Task<ServiceResult> CreateDepartment(DepartmentDto dto, string token)
        {
            ServiceResult res = new ServiceResult();
            string query = "";
            DynamicParameters filter = new DynamicParameters();
            string resultMessage = "";
            try
            {
                if (dto.Id == 0)
                {
                    query = @"Insert Into Departmnet (Description) values
                        ( Description=@Description)";
                    filter.Add("Description", dto.DepartmentName);
                    resultMessage = "Kayıt işlemi başarılı";
                }
                else
                {
                    query = "update Departmnet set Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("Description", dto.DepartmentName);
                    resultMessage = "Güncelleme işlemi başarılı";
                }
                var result = _db.Execute(query, filter);
                res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "CreateDepartment", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Departman bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return res;
        }

        public async Task<ServiceResult> GetTitle()
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select Id, Description from Titles";
                var dataList = await _db.QueryAsync<TitleDto>(query) as List<TitleDto>;
                res = Result.ReturnAsSuccess(data: dataList);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetTitle", PlatformType.Web);
            }
            return res;
        }
        public async Task<ServiceResult> CreateTitle(TitleDto dto)
        {
            ServiceResult res = new ServiceResult();
            string query = "";
            DynamicParameters filter = new DynamicParameters();
            string resultMessage = "";
            try
            {
                if (dto.Id == 0)
                {
                    query = @"insert into Titles (Description) values (@Description);";
                    filter.Add("Description", dto.Description);
                    resultMessage = "Kayıt işlemi başarılı";
                }
                else
                {
                    query = "update Titles set Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("Description", dto.Description);
                    resultMessage = "Güncelleme işlemi başarılı";
                }
                var result = _db.Execute(query, filter);
                res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "CreateDepartment", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Ünvan bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return res;
        }

        public async Task<ServiceResult> UpdateDepartmentIsActive(long Id, bool IsActive)
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                string resultMessage = "";

                //var data = _departmentRepo.Get(x => x.Id == Id);
                //data.IsActive = IsActive;

                //_departmentRepo.Update(_mapper.Map<Department>(data));
                //_unitOfWork.SaveChanges();
                resultMessage = AlertResource.UpdateIsOk;
                res = Result.ReturnAsSuccess(null, resultMessage, null);
            });
            return res;
        }
        #endregion

        #region Faculty
        public async Task<ServiceResult> GetFaculty(string token)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select Id, Description as FacultyName from Faculty";
                var reullt = await _db.QueryAsync<FacultyDto>(query) as List<FacultyDto>;
                res = Result.ReturnAsSuccess(data: reullt);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetFaculty", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Fakülte bilgisi yüklenirken hata meydana geldi");
            }
            return res;
        }

        public async Task<List<FacultyDto>> GetFacultyForUI()
        {
            List<FacultyDto> res = new List<FacultyDto>();
            try
            {
                string query = "select Id, Description as FacultyName from Faculty";
                res = await _db.QueryAsync<FacultyDto>(query) as List<FacultyDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetFacultyForUI", PlatformType.Web);
            }
            return res;
        }

        public async Task<ServiceResult> CreateFaculty(FacultyDto dto, string token)
        {
            ServiceResult res = new ServiceResult();
            string query = "";
            DynamicParameters filter = new DynamicParameters();
            string resultMessage = "";
            try
            {
                if (dto.Id == 0)
                {
                    query = $"Insert Into Faculty (Id,Description) values" +
                        $"(Id={await GetFacultId()}, Description='{dto.FacultyName}')";
                }
                else
                {
                    query = "update Faculty set Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("Description", dto.FacultyName);
                    resultMessage = "Güncelleme işlemi başarılı";
                }
                var result = await _db.ExecuteAsync(query);
                res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "CreateFaculty", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Fakülte bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return res;
        }

        public async Task<long> GetFacultId()
        {
            long res = 0;
            try
            {
                var value = await _db.QueryAsync<long>("select Id+1 as Id from Faculty order by Id desc limit 1");
                res = Convert.ToInt64(value.First());
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "CreateFaculty", PlatformType.Web);
            }
            return res;
        }


        public async Task<ServiceResult> UpdateFacultIsActive(long Id, bool IsActive)
        {
            ServiceResult res = new ServiceResult();
            await Task.Run(() =>
            {
                string resultMessage = "";

                //var data = _facultyRepo.Get(x => x.Id == Id);
                //data.IsActive = IsActive;

                //_facultyRepo.Update(_mapper.Map<Faculty>(data));
                //_unitOfWork.SaveChanges();
                resultMessage = AlertResource.UpdateIsOk;
                res = Result.ReturnAsSuccess(message: resultMessage);
            });
            return res;
        }

        #endregion

        #region HighSchool
        public async Task<ServiceResult> GetHighSchool(string token)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select Id, Description as SchoolName from HighSchool";
                var result = await _db.QueryAsync<HighSchoolDto>(query) as List<HighSchoolDto>;
                res = Result.ReturnAsSuccess(data: result);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "GetHighSchool", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Lise bilgisi yüklenirken hata meydana geldi");
            }
            return res;
        }

        public async Task<List<HighSchoolDto>> GetHighSchoolForUI()
        {
            List<HighSchoolDto> res = new List<HighSchoolDto>();
            try
            {
                string query = "select Id, Description as SchoolName from HighSchool";
                res = await _db.QueryAsync<HighSchoolDto>(query) as List<HighSchoolDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetHighSchoolForUI", PlatformType.Web);
            }
            return res;
        }

        public async Task<ServiceResult> CreateHighSchool(HighSchoolDto dto, string token)
        {
            ServiceResult res = new ServiceResult();
            string query = "";
            DynamicParameters filter = new DynamicParameters();
            string resultMessage = "";
            try
            {
                if (dto.Id == 0)
                {
                    query = @"Insert Into HighSchool (  Description) values ( Description=@Description)";
                    filter.Add("Description", dto.SchoolName);
                    resultMessage = "Kayıt işlemi başarılı";
                }
                else
                {
                    query = "update HighSchool set Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("Description", dto.SchoolName);
                    resultMessage = "Güncelleme işlemi başarılı";
                }
                var result = _db.Execute(query, filter);
                res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "CreateHighSchool", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Lise bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return res;
        }

        public async Task<ServiceResult> UpdatehighSchoolIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            //var data = _highSchoolTypeRepo.Get(x => x.Id == Id);
            //data.IsActive = IsActive;

            //_highSchoolTypeRepo.Update(_mapper.Map<HighSchool>(data));
            //_unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(null, resultMessage, null);
        }

        #endregion

        #region StudentClass
        public async Task<ServiceResult> GetStudentClass(string token)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select * from StudentClass";
                var result = await _db.QueryAsync<StudentClassDto>(query) as List<StudentClassDto>;

                res = Result.ReturnAsSuccess(data: result);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "GetStudentClass", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Sınıf bilgisi yüklenirken hata meydana geldi");
            }
            return res;
        }

        public async Task<List<StudentClassDto>> GetStudentClassForUI()
        {
            List<StudentClassDto> res = new List<StudentClassDto>();
            try
            {
                string query = "select * from StudentClass";
                res = await _db.QueryAsync<StudentClassDto>(query) as List<StudentClassDto>;

            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetUniversityForUI", PlatformType.Web);
            }
            return res;
        }

        public async Task<ServiceResult> CreateStudentClass(StudentClassDto dto, string token)
        {
            ServiceResult res = new ServiceResult();
            string query = "";
            DynamicParameters filter = new DynamicParameters();
            string resultMessage = "";
            try
            {
                if (dto.Id == 0)
                {
                    query = @"Insert Into StudentClass ( Description) values
                        ( Description=@Description)";
                    filter.Add("Description", dto.Description);
                    resultMessage = "Kayıt işlemi başarılı";
                }
                else
                {
                    query = "update StudentClass set Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("Description", dto.Description);
                    resultMessage = "Güncelleme işlemi başarılı";
                }
                var result = _db.Execute(query, filter);
                res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "CreateStudentClass", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Sınıf bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return res;
        }

        public async Task<ServiceResult> UpdateStudentClassIsActive(long Id, bool IsActive)
        {

            string resultMessage = "";

            //var data = _educationStatusRepo.Get(x => x.Id == Id);
            //data.IsActive = IsActive;

            //_classTypeRepo.Update(_mapper.Map<StudentClass>(data));
            //_unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(null, resultMessage, null);
        }

        #endregion

        #region University
        public async Task<ServiceResult> GetUniversity(string token)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select Id, Description as UniversityName from Universities";
                var data = _db.Query<UniversityDto>(query).ToList();
                res = Result.ReturnAsSuccess(data: data);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "GetUniversity", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Sınıf bilgisi yüklenirken hata meydana geldi");
            }
            return res;
        }

        public async Task<List<UniversityDto>> GetUniversityForUI()
        {
            List<UniversityDto> res = new List<UniversityDto>();
            try
            {
                string query = "select Id, Description as UniversityName from Universities";
                res = await _db.QueryAsync<UniversityDto>(query) as List<UniversityDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetUniversityForUI", PlatformType.Web);
            }
            return res;
        }

        public async Task<ServiceResult> CreateUniversity(UniversityDto dto, string token)
        {
            ServiceResult res = new ServiceResult();
            string query = "";
            DynamicParameters filter = new DynamicParameters();
            string resultMessage = "";
            try
            {
                if (dto.Id == 0)
                {
                    query = @"Insert Into Universities ( Description) values
                        ( Description=@Description)";
                    filter.Add("Description", dto.UniversityName);
                    resultMessage = "Kayıt işlemi başarılı";
                }
                else
                {
                    query = "update Universities set Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("Description", dto.UniversityName);
                    resultMessage = "Güncelleme işlemi başarılı";
                }
                var result = _db.Execute(query, filter);
                res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "CreateUniversity", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Üniversite bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return res;
        }

        public async Task<ServiceResult> UpdateUniversityIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            //var data = _universityRepo.Get(x => x.Id == Id);
            //data.IsActive = IsActive;

            //_universityRepo.Update(_mapper.Map<University>(data));
            //_unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(null, resultMessage, null);
        }

        #endregion

        #region Sector
        public async Task<ServiceResult> GetAllSector(string token)
        {
            ServiceResult res = new ServiceResult();
            try
            {
                string query = "select * from Sector";
                var result = await _db.QueryAsync<SectorDto>(query) as List<SectorDto>;
                res = Result.ReturnAsSuccess(data: result);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "GetAllSector", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Sektör bilgisi yüklenirken hata meydana geldi");
            }
            return res;
        }

        public async Task<List<SectorDto>> GetAllSectorForUI()
        {
            List<SectorDto> res = new List<SectorDto>();
            try
            {
                string query = "select * from Sector";
                res = await _db.QueryAsync<SectorDto>(query) as List<SectorDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, null, "GetAllSectorForUI", PlatformType.Mobil);
            }
            return res;
        }

        public async Task<ServiceResult> CreateSector(SectorDto dto, string token)
        {
            ServiceResult res = new ServiceResult();
            string query = "";
            DynamicParameters filter = new DynamicParameters();
            string resultMessage = "";
            try
            {
                if (dto.Id == 0)
                {
                    query = @"Insert Into Sector ( CreatedDate, Description) values
                        ( @CreatedDate, @Description)";
                    filter.Add("CreatedDate", DateTime.Now);
                    filter.Add("Description", dto.Description);
                    resultMessage = "Kayıt işlemi başarılı";
                }
                else
                {
                    query = "update Sector set Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("Description", dto.Description);
                    resultMessage = "Güncelleme işlemi başarılı";
                }
                var result = _db.Execute(query, filter);
                res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex, JwtToken.GetUserId(token), "CreateSector", PlatformType.Web);
                res = Result.ReturnAsFail(message: "Sektör bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return res;
        }
        #endregion

    }
}
