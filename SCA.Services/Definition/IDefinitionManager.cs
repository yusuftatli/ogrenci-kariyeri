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
        Task<ServiceResult> GetDepartment(UserSession session);
        Task<List<DepartmentDto>> GetDepartmentForUI();
        Task<ServiceResult> CreateDepartment(DepartmentDto dto, UserSession session);
        Task<ServiceResult> UpdateDepartmentIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetFaculty(UserSession session);
        Task<List<FacultyDto>> GetFacultyForUI();
        Task<ServiceResult> CreateFaculty(FacultyDto dto, UserSession session);
        Task<ServiceResult> UpdateFacultIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetHighSchool(UserSession session);
        Task<List<HighSchoolDto>> GetHighSchoolForUI();
        Task<ServiceResult> CreateHighSchool(HighSchoolDto dto, UserSession session);
        Task<ServiceResult> UpdatehighSchoolIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetStudentClass(UserSession session);
        Task<List<StudentClassDto>> GetStudentClassForUI();
        Task<ServiceResult> CreateStudentClass(StudentClassDto dto, UserSession session);
        Task<ServiceResult> UpdateStudentClassIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetUniversity(UserSession session);
        Task<List<UniversityDto>> GetUniversityForUI();
        Task<ServiceResult> CreateUniversity(UniversityDto dto, UserSession session);
        Task<ServiceResult> UpdateUniversityIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetAllSector(UserSession session);
        Task<List<SectorDto>> GetAllSectorForUI();
        Task<ServiceResult> CreateSector(SectorDto dto, UserSession session);

    }
}
