using AutoMapper;
using Dapper;
using MySql.Data.MySqlClient;
using SCA.Common.Resource;
using SCA.Common.Result;
using SCA.Entity.DTO;
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
        public async Task<ServiceResult> GetDepartment()
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select Id, Description as DepartmentName from Departmnet";
                var result = _db.Query<DepartmentDto>(query).ToList();

                if (result.Count > 0)
                {
                    _res = Result.ReturnAsSuccess(data: result);
                }
                else
                {
                    _res = Result.ReturnAsFail(message: "Departman bilgisi yüklenemedi");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: "Departman bilgisi yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<List<DepartmentDto>> GetDepartmentForUI()
        {
            List<DepartmentDto> _res = new List<DepartmentDto>();
            try
            {
                string query = "select * from Departmnet";
                var result = _db.Query<DepartmentDto>(query).ToList();
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
                    query = @"Insert Into Departmnet ( CreatedUserId, CreatedDate, Description) values
                        ( CreatedUserId=@CreatedUserId, CreatedDate=@CreatedDate,Description=@Description)";
                    filter.Add("CreatedUserId", session.Id);
                    filter.Add("CreatedDate", DateTime.Now);
                    filter.Add("Description", dto.DepartmentName);
                    resultMessage = "Kayıt işlemi başarılı";
                }
                else
                {
                    query = "update Departmnet set UpdatedUserId=@UpdatedUserId,UpdatedDate=@UpdatedDate ,Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("UpdatedDate", session.Id);
                    filter.Add("CreatedDate", DateTime.Now);
                    filter.Add("Description", dto.DepartmentName);
                    resultMessage = "Güncelleme işlemi başarılı";
                }
                var res = _db.Execute(query, filter);
                _res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
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
        public async Task<ServiceResult> GetFaculty()
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from Faculty";
                var result = _db.Query<FacultyDto>(query).ToList();

                if (result.Count > 0)
                {
                    _res = Result.ReturnAsSuccess(data: result);
                }
                else
                {
                    _res = Result.ReturnAsFail(message: "Fakülte bilgisi yüklenemedi");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
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
                var result = _db.Query<FacultyDto>(query).ToList();
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
                    query = @"Insert Into Faculty ( CreatedUserId, CreatedDate, Description) values
                        ( CreatedUserId=@CreatedUserId, CreatedDate=@CreatedDate,Description=@Description)";
                    filter.Add("CreatedUserId", session.Id);
                    filter.Add("CreatedDate", DateTime.Now);
                    filter.Add("Description", dto.FacultyName);
                    resultMessage = "Kayıt işlemi başarılı";
                }
                else
                {
                    query = "update Faculty set UpdatedUserId=@UpdatedUserId,UpdatedDate=@UpdatedDate ,Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
                    filter.Add("UpdatedDate", session.Id);
                    filter.Add("CreatedDate", DateTime.Now);
                    filter.Add("Description", dto.FacultyName);
                    resultMessage = "Güncelleme işlemi başarılı";
                }
                var res = _db.Execute(query, filter);
                _res = Result.ReturnAsSuccess(message: resultMessage);
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
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
        public async Task<ServiceResult> GetHighSchool()
        {
            var dataList = _mapper.Map<List<HighSchoolDto>>(_highSchoolTypeRepo.GetAll().ToList());
            var cityList = await _addressManager.CityList();
            dataList.ForEach(x =>
            {
                x.CityName = cityList.Where(a => a.CityId == x.CityId).Select(s => s.CityName).FirstOrDefault();
            });
            return Result.ReturnAsSuccess(null, null, dataList);
        }

        public async Task<List<HighSchoolDto>> GetHighSchoolForUI()
        {
            var dataList = _mapper.Map<List<HighSchoolDto>>(_highSchoolTypeRepo.GetAll().ToList());
            var cityList = await _addressManager.CityList();
            dataList.ForEach(x =>
            {
                x.CityName = cityList.Where(a => a.CityId == x.CityId).Select(s => s.CityName).FirstOrDefault();
            });
            return dataList;
        }

        public async Task<ServiceResult> CreateHighSchool(HighSchoolDto dto)
        {
            string resultMessage = "";
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            if (dto.Id == 0)
            {
                dto.IsActive = true;
                _highSchoolTypeRepo.Add(_mapper.Map<HighSchool>(dto));
                resultMessage = AlertResource.CreateIsOk;
            }
            else
            {
                _highSchoolTypeRepo.Update(_mapper.Map<HighSchool>(dto));
                resultMessage = AlertResource.UpdateIsOk;
            }
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(null, resultMessage, null);
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
        public async Task<ServiceResult> GetStudentClass()
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from StudentClass";
                var result = _db.Query<StudentClassDto>(query).ToList();

                if (result.Count > 0)
                {
                    _res = Result.ReturnAsSuccess(data: result);
                }
                else
                {
                    _res = Result.ReturnAsFail(message: "Sınıf bilgisi yüklenemedi");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: "Sınıf bilgisi yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<List<StudentClassDto>> GetStudentClassForUI()
        {
            return _mapper.Map<List<StudentClassDto>>(_classTypeRepo.GetAll().ToList());
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
                    query = @"Insert Into StudentClass ( CreatedUserId, CreatedDate, Description) values
                        ( CreatedUserId=@CreatedUserId, CreatedDate=@CreatedDate,Description=@Description)";
                    filter.Add("CreatedUserId", session.Id);
                    filter.Add("CreatedDate", DateTime.Now);
                    filter.Add("Description", dto.Description);
                    resultMessage = "Kayıt işlemi başarılı";
                }
                else
                {
                    query = "update StudentClass set UpdatedUserId=@UpdatedUserId,UpdatedDate=@UpdatedDate ,Description=@Description where Id=@Id";
                    filter.Add("Id", dto.Id);
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
                await _errorManagement.SaveError(ex.ToString());
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
        public async Task<ServiceResult> GetUniversity()
        {
            var dataList = _mapper.Map<List<UniversityDto>>(_universityRepo.GetAll().ToList());
            var cityList = await _addressManager.CityList();
            dataList.ForEach(x =>
            {
                x.CityName = cityList.Where(a => a.CityId == x.CityId).Select(s => s.CityName).FirstOrDefault();
            });
            return Result.ReturnAsSuccess(null, null, dataList);
        }

        public async Task<List<UniversityDto>> GetUniversityForUI()
        {
            var dataList = _mapper.Map<List<UniversityDto>>(_universityRepo.GetAll().ToList());
            var cityList = await _addressManager.CityList();
            dataList.ForEach(x =>
            {
                x.CityName = cityList.Where(a => a.CityId == x.CityId).Select(s => s.CityName).FirstOrDefault();
            });
            return dataList;
        }

        public async Task<ServiceResult> CreateUniversity(UniversityDto dto)
        {
            string resultMessage = "";
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            if (dto.Id == 0)
            {
                dto.IsActive = true;
                _universityRepo.Add(_mapper.Map<University>(dto));
                resultMessage = AlertResource.CreateIsOk;
            }
            else
            {
                _universityRepo.Update(_mapper.Map<University>(dto));
                resultMessage = AlertResource.UpdateIsOk;
            }
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(null, resultMessage, null);
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
        public async Task<ServiceResult> GetAllSector()
        {
            ServiceResult _res = new ServiceResult();
            try
            {
                string query = "select * from Sector";
                var result = _db.Query<SectorDto>(query).ToList();

                if (result.Count > 0)
                {
                    _res = Result.ReturnAsSuccess(data: result);
                }
                else
                {
                    _res = Result.ReturnAsFail(message: "Sektör bilgisi yüklenemedi");
                }
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: "Sektör bilgisi yüklenirken hata meydana geldi");
            }
            return _res;
        }

        public async Task<List<SectorDto>> GetAllSectorForUI()
        {
            List<SectorDto> listData = new List<SectorDto>();
            try
            {
                string query = "select * from Sector";
                listData = _db.Query<SectorDto>(query).ToList();
            }
            catch (Exception ex)
            {
                await _errorManagement.SaveError(ex.ToString());
            }
            return listData;
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
                        (SectorTypeId=@SectorTypeId, CreatedUserId=@CreatedUserId, CreatedDate=@CreatedDate,Description=@Description)";
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
                await _errorManagement.SaveError(ex.ToString());
                _res = Result.ReturnAsFail(message: "Sektör bilgisi kayıt işlemi sırasında hata meydana geldi.");
            }
            return _res;
        }
        #endregion

    }
}
