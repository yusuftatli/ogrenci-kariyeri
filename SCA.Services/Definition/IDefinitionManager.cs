using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface IDefinitionManager
    {
        Task<ServiceResult> GetDepartment();
        Task<List<DepartmentDto>> GetDepartmentForUI();
        Task<ServiceResult> CreateDepartment(DepartmentDto dto);
        Task<ServiceResult> UpdateDepartmentIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetEducationStatus();
        Task<List<EducationStatusDto>> GetEducationStatusForUI();
        Task<ServiceResult> CreateEducationStatus(EducationStatusDto dto);
        Task<ServiceResult> UpdateEducationStatusIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetFaculty();
        Task<List<FacultyDto>> GetFacultyForUI();
        Task<ServiceResult> CreateFaculty(FacultyDto dto);
        Task<ServiceResult> UpdateFacultIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetHighSchool();
        Task<List<HighSchoolDto>> GetHighSchoolForUI();
        Task<ServiceResult> CreateHighSchool(HighSchoolDto dto);
        Task<ServiceResult> UpdatehighSchoolIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetStudentClass();
        Task<List<StudentClassDto>> GetStudentClassForUI();
        Task<ServiceResult> CreateStudentClass(StudentClassDto dto);
        Task<ServiceResult> UpdateStudentClassIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetUniversity();
        Task<List<UniversityDto>> GetUniversityForUI();
        Task<ServiceResult> CreateUniversity(UniversityDto dto);
        Task<ServiceResult> UpdateUniversityIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetAllSector();
        Task<List<Sector>> GetAllSectorForUI();
        Task<ServiceResult> CreateSector(SectorDto dto);

    }
}
