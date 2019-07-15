using AutoMapper;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class EducationManager : IEducationManager
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
        public EducationManager(IUnitofWork unitOfWork, IMapper mapper, IAddressManager addressManager)
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

        }

        #region Department
        public async Task<ServiceResult> GetDepartment()
        {
            return Result.ReturnAsSuccess(null, _mapper.Map<List<DepartmentDto>>(_departmentRepo.GetAll().ToList()));
        }

        public async Task<ServiceResult> CreateDepartment(DepartmentDto dto)
        {
            string resultMessage = "";
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            if (dto.Id == 0)
            {
                dto.IsActive = true;
                _departmentRepo.Add(_mapper.Map<Department>(dto));
                resultMessage = AlertResource.CreateIsOk;
            }
            else
            {
                _departmentRepo.Update(_mapper.Map<Department>(dto));
                resultMessage = AlertResource.UpdateIsOk;
            }
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(resultMessage, null);
        }

        public async Task<ServiceResult> UpdateDepartmentIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            var data = _departmentRepo.Get(x => x.Id == Id);
            data.IsActive = IsActive;

            _departmentRepo.Update(_mapper.Map<Department>(data));
            _unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(resultMessage, null);
        }
        #endregion

        #region EducationStatus
        public async Task<ServiceResult> GetEducationStatus()
        {
            return Result.ReturnAsSuccess(null, _mapper.Map<List<EducationStatusDto>>(_educationStatusRepo.GetAll().ToList()));
        }

        public async Task<ServiceResult> CreateEducationStatus(EducationStatusDto dto)
        {
            string resultMessage = "";
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            if (dto.Id == 0)
            {
                _educationStatusRepo.Add(_mapper.Map<EducationStatus>(dto));
                resultMessage = AlertResource.CreateIsOk;
            }
            else
            {
                _educationStatusRepo.Update(_mapper.Map<EducationStatus>(dto));
                resultMessage = AlertResource.UpdateIsOk;
            }
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(resultMessage, null);
        }

        public async Task<ServiceResult> UpdateEducationStatusIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            var data = _educationStatusRepo.Get(x => x.Id == Id);
            data.IsActive = IsActive;

            _educationStatusRepo.Update(_mapper.Map<EducationStatus>(data));
            _unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(resultMessage, null);
        }
        #endregion

        #region Faculty
        public async Task<ServiceResult> GetFaculty()
        {
            var dataList = _mapper.Map<List<FacultyDto>>(_facultyRepo.GetAll().ToList());
            return Result.ReturnAsSuccess(null, dataList);
        }

        public async Task<ServiceResult> CreateFaculty(FacultyDto dto)
        {
            string resultMessage = "";
            if (dto == null)
            {
                dto.IsActive = true;
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            if (dto.Id == 0)
            {
                _facultyRepo.Add(_mapper.Map<Faculty>(dto));
                resultMessage = AlertResource.CreateIsOk;
            }
            else
            {
                _facultyRepo.Update(_mapper.Map<Faculty>(dto));
                resultMessage = AlertResource.UpdateIsOk;
            }
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(resultMessage, null);
        }

        public async Task<ServiceResult> UpdateFacultIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            var data = _facultyRepo.Get(x => x.Id == Id);
            data.IsActive = IsActive;

            _facultyRepo.Update(_mapper.Map<Faculty>(data));
            _unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(resultMessage, null);
        }

        #endregion

        #region HighSchool
        public async Task<ServiceResult> GetHighSchool()
        {
            var dataList = _mapper.Map<List<HighSchoolDto>>(_highSchoolTypeRepo.GetAll().ToList());
            var cityList = _addressManager.CityList();
            dataList.ForEach(x =>
            {
                x.CityName = cityList.Where(a => a.CityId == x.CityId).Select(s => s.CityName).FirstOrDefault();
            });
            return Result.ReturnAsSuccess(null, dataList);
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
            return Result.ReturnAsSuccess(resultMessage, null);
        }

        public async Task<ServiceResult> UpdatehighSchoolIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            var data = _highSchoolTypeRepo.Get(x => x.Id == Id);
            data.IsActive = IsActive;

            _highSchoolTypeRepo.Update(_mapper.Map<HighSchool>(data));
            _unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(resultMessage, null);
        }

        #endregion

        #region StudentClass
        public async Task<ServiceResult> GetStudentClass()
        {
            return Result.ReturnAsSuccess(null, _mapper.Map<List<StudentClassDto>>(_classTypeRepo.GetAll().ToList()));
        }

        public async Task<ServiceResult> CreateStudentClass(StudentClassDto dto)
        {
            string resultMessage = "";
            if (dto == null)
            {
                Result.ReturnAsFail(AlertResource.NoChanges, null);
            }
            if (dto.Id == 0)
            {
                _classTypeRepo.Add(_mapper.Map<StudentClass>(dto));
                resultMessage = AlertResource.CreateIsOk;
            }
            else
            {
                _classTypeRepo.Update(_mapper.Map<StudentClass>(dto));
                resultMessage = AlertResource.UpdateIsOk;
            }
            _unitOfWork.SaveChanges();
            return Result.ReturnAsSuccess(resultMessage, null);
        }

        public async Task<ServiceResult> UpdateStudentClassIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            var data = _educationStatusRepo.Get(x => x.Id == Id);
            data.IsActive = IsActive;

            _classTypeRepo.Update(_mapper.Map<StudentClass>(data));
            _unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(resultMessage, null);
        }

        #endregion

        #region University
        public async Task<ServiceResult> GetUniversity()
        {
            var dataList = _mapper.Map<List<UniversityDto>>(_universityRepo.GetAll().ToList());
            var cityList = _addressManager.CityList();
            dataList.ForEach(x =>
            {
                x.CityName = cityList.Where(a => a.CityId == x.CityId).Select(s => s.CityName).FirstOrDefault();
            });
            return Result.ReturnAsSuccess(null, dataList);
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
            return Result.ReturnAsSuccess(resultMessage, null);
        }

        public async Task<ServiceResult> UpdateUniversityIsActive(long Id, bool IsActive)
        {
            string resultMessage = "";

            var data = _universityRepo.Get(x => x.Id == Id);
            data.IsActive = IsActive;

            _universityRepo.Update(_mapper.Map<University>(data));
            _unitOfWork.SaveChanges();
            resultMessage = AlertResource.UpdateIsOk;
            return Result.ReturnAsSuccess(resultMessage, null);
        }

        #endregion

    }
}
