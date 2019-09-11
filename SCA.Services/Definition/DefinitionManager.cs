using AutoMapper;
using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using SCA.Entity.Model;
using SCA.Model;
using SCA.Repository.Repo;
using SCA.Repository.UoW;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class DefinitionManager : IDefinitionManager
    {
        private readonly IMapper _mapper;
        private readonly IUnitofWork _unitOfWork;
        private readonly IAddressManager _addressManager;
        private IGenericRepository<Department> _departmentRepo;
        private IGenericRepository<EducationStatus> _educationStatusRepo;
        private IGenericRepository<Faculty> _facultyRepo;
        private IGenericRepository<HighSchool> _highSchoolTypeRepo;
        private IGenericRepository<StudentClass> _classTypeRepo;
        private IGenericRepository<University> _universityRepo;
        private IGenericRepository<Sector> _sectorRepo;
        private readonly IErrorManagement _errorManagement;
        private readonly IDbConnection _db = new MySqlConnection("Server=167.71.46.71;Database=StudentDbTest;Uid=ogrencikariyeri;Pwd=dXog323!s.?;");

        public DefinitionManager(IUnitofWork unitOfWork, IMapper mapper, IAddressManager addressManager, IErrorManagement errorManagement)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _departmentRepo = unitOfWork.GetRepository<Department>();
            _educationStatusRepo = unitOfWork.GetRepository<EducationStatus>();
            _facultyRepo = unitOfWork.GetRepository<Faculty>();
            _highSchoolTypeRepo = unitOfWork.GetRepository<HighSchool>();
            _classTypeRepo = unitOfWork.GetRepository<StudentClass>();
            _universityRepo = unitOfWork.GetRepository<University>();
            _addressManager = addressManager;
            _sectorRepo = unitOfWork.GetRepository<Sector>();
            _errorManagement = errorManagement;

        }

        #region Department
        public async Task<ServiceResult> GetDepartment(UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select Id, Description as DepartmentName from Departmnet";
                var result = await _db.QueryAsync<DepartmentDto>(query) as List<DepartmentDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "GetDepartment", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Departman bilgisi yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<List<DepartmentDto>> GetDepartmentForUI()
        {
            List<DepartmentDto> _res = new List<DepartmentDto>();
            try
            {
                string query = "select Id, Description as DepartmentName from Departmnet";
                _res = await _db.QueryAsync<DepartmentDto>(query) as List<DepartmentDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        public async Task<ServiceResult> CreateDepartment(DepartmentDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
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
                var res = _db.Execute(query, filter);
                _res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "CreateDepartment", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Departman bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return _res;
        }

        public async Task<ServiceResult> UpdateDepartmentIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            var data = _departmentRepo.Get(x => x.Id == Id);
            data.IsActive = IsActive;

            _departmentRepo.Update(_mapper.Map<Department>(data));
            _unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(null, resultMessage, null);
        }
        #endregion

        #region Faculty
        public async Task<ServiceResult> GetFaculty(UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from Faculty";
                var reullt = await _db.QueryAsync<FacultyDto>(query) as List<FacultyDto>;
                _res = Result.ReturnAsSuccess(data: reullt);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "GetFaculty", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Fakülte bilgisi yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<List<FacultyDto>> GetFacultyForUI()
        {
            List<FacultyDto> _res = new List<FacultyDto>();
            try
            {
                string query = "select Id, Description as FacultyName from Faculty";
                _res = await _db.QueryAsync<FacultyDto>(query) as List<FacultyDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        public async Task<ServiceResult> CreateFaculty(FacultyDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            string query = "";
            DynamicParameters filter = new DynamicParameters();
            string resultMessage = "";
            try
            {
                if (dto.Id == 0)
                {
                    query = @"Insert Into Faculty (Description) values
                        (Description=@Description)";
                    filter.Add("Description", dto.FacultyName);
                    resultMessage = "Kayıt işlemi başarılı";
                }
                else
                {
                    query = "update Faculty set Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("Description", dto.FacultyName);
                    resultMessage = "Güncelleme işlemi başarılı";
                }
                var res = _db.Execute(query, filter);
                _res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "CreateFaculty", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Fakülte bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return _res;
        }

        public async Task<ServiceResult> UpdateFacultIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            var data = _facultyRepo.Get(x => x.Id == Id);
            data.IsActive = IsActive;

            _facultyRepo.Update(_mapper.Map<Faculty>(data));
            _unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(null, resultMessage, null);
        }

        #endregion

        #region HighSchool
        public async Task<ServiceResult> GetHighSchool(UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select Id, Description as SchoolName from HighSchool";
                var result = await _db.QueryAsync<HighSchoolDto>(query) as List<HighSchoolDto>;
                _res = Result.ReturnAsSuccess(data: result);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "GetHighSchool", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Lise bilgisi yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<List<HighSchoolDto>> GetHighSchoolForUI()
        {
            List<HighSchoolDto> _res = new List<HighSchoolDto>();
            try
            {
                string query = "select Id, Description as SchoolName from HighSchool";
                _res = await _db.QueryAsync<HighSchoolDto>(query) as List<HighSchoolDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        public async Task<ServiceResult> CreateHighSchool(HighSchoolDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
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
                var res = _db.Execute(query, filter);
                _res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "CreateHighSchool", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Lise bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return _res;
        }

        public async Task<ServiceResult> UpdatehighSchoolIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            var data = _highSchoolTypeRepo.Get(x => x.Id == Id);
            data.IsActive = IsActive;

            _highSchoolTypeRepo.Update(_mapper.Map<HighSchool>(data));
            _unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(null, resultMessage, null);
        }

        #endregion

        #region StudentClass
        public async Task<ServiceResult> GetStudentClass(UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from StudentClass";
                var result = await _db.QueryAsync<StudentClassDto>(query) as List<StudentClassDto>;

                if (result.Count > 0)
                {
                    _res = Result.ReturnAsSuccess(data: result);
                }
                else
                {
                    _res = Result.ReturnAsSuccess(message: "Sınıf bilgisi yüklenemedi");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "GetStudentClass", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Sınıf bilgisi yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<List<StudentClassDto>> GetStudentClassForUI()
        {
            List<StudentClassDto> _res = new List<StudentClassDto>();
            try
            {
                string query = "select * from StudentClass";
                _res = await _db.QueryAsync<StudentClassDto>(query) as List<StudentClassDto>;

            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        public async Task<ServiceResult> CreateStudentClass(StudentClassDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
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
                var res = _db.Execute(query, filter);
                _res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "CreateStudentClass", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Sınıf bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return _res;
        }

        public async Task<ServiceResult> UpdateStudentClassIsActive(long Id, bool IsActive)
        {

            string resultMessage = "";

            var data = _educationStatusRepo.Get(x => x.Id == Id);
            data.IsActive = IsActive;

            _classTypeRepo.Update(_mapper.Map<StudentClass>(data));
            _unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(null, resultMessage, null);
        }

        #endregion

        #region University
        public async Task<ServiceResult> GetUniversity(UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select Id, Description as UniversityName from Universities";
                var data = _db.Query<StudentClassDto>(query).ToList();
                _res = Result.ReturnAsSuccess(data: data);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "GetUniversity", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Sınıf bilgisi yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<List<UniversityDto>> GetUniversityForUI()
        {
            List<UniversityDto> _res = new List<UniversityDto>();
            try
            {
                string query = "select Id, Description as UniversityName from Universities";
                _res = await _db.QueryAsync<UniversityDto>(query) as List<UniversityDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        public async Task<ServiceResult> CreateUniversity(UniversityDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
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
                var res = _db.Execute(query, filter);
                _res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "CreateUniversity", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Üniversite bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return _res;
        }

        public async Task<ServiceResult> UpdateUniversityIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            var data = _universityRepo.Get(x => x.Id == Id);
            data.IsActive = IsActive;

            _universityRepo.Update(_mapper.Map<University>(data));
            _unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(null, resultMessage, null);
        }

        #endregion

        #region Sector
        public async Task<ServiceResult> GetAllSector(UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from Sector";
                var result = await _db.QueryAsync<SectorDto>(query) as List<SectorDto>;
                _res = Result.ReturnAsSuccess(data: result);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "GetAllSector", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Sektör bilgisi yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<List<SectorDto>> GetAllSectorForUI()
        {
            List<SectorDto> _res = new List<SectorDto>();
            try
            {
                string query = "select * from Sector";
                _res = await _db.QueryAsync<SectorDto>(query) as List<SectorDto>;
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return _res;
        }

        public async Task<ServiceResult> CreateSector(SectorDto dto, UserSession session)
        {
            ServiceResult _res = new ServiceResult();
            string query = "";
            DynamicParameters filter = new DynamicParameters();
            string resultMessage = "";
            try
            {
                if (dto.Id == 0)
                {
                    query = @"Insert Into Sector (SectorTypeId, CreatedUserId, CreatedDate, Description) values
                        (@SectorTypeId, @CreatedUserId, @CreatedDate, @Description)";
                    filter.Add("SectorTypeId", dto.SectorTypeId);
                    filter.Add("CreatedUserId", session.Id);
                    filter.Add("CreatedDate", DateTime.Now);
                    filter.Add("Description", dto.Description);
                    resultMessage = "Kayıt işlemi başarılı";
                }
                else
                {
                    query = "update Sector set SectorTypeId=@SectorTypeId,UpdatedUserId=@UpdatedUserId,UpdatedDate=@UpdatedDate ,Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("SectorTypeId", dto.SectorTypeId);
                    filter.Add("UpdatedDate", session.Id);
                    filter.Add("CreatedDate", DateTime.Now);
                    filter.Add("Description", dto.Description);
                    resultMessage = "Güncelleme işlemi başarılı";
                }
                var res = _db.Execute(query, filter);
                _res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString(), session.Id, "CreateSector", PlatformType.Web);
                _res = Result.ReturnAsFail(message: "Sektör bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return _res;
        }
        #endregion

    }
}
