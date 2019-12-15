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
        Task<ServiceResult> GetDepartment(string token);
        Task<ServiceResult> GetDepartmentForUI1();
        Task<List<DepartmentDto>> GetDepartmentForUI();
        Task<ServiceResult> CreateDepartment(DepartmentDto dto, string token);
        Task<ServiceResult> UpdateDepartmentIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetFaculty(string token);
        Task<List<FacultyDto>> GetFacultyForUI();
        Task<ServiceResult> CreateFaculty(FacultyDto dto, string token);
        Task<ServiceResult> UpdateFacultIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetHighSchool(string token);
        Task<List<HighSchoolDto>> GetHighSchoolForUI();
        Task<ServiceResult> CreateHighSchool(HighSchoolDto dto, string token);
        Task<ServiceResult> UpdatehighSchoolIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetStudentClass(string token);
        Task<List<StudentClassDto>> GetStudentClassForUI();
        Task<ServiceResult> CreateStudentClass(StudentClassDto dto, string token);
        Task<ServiceResult> UpdateStudentClassIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetUniversity(string token);
        Task<List<UniversityDto>> GetUniversityForUI();
        Task<ServiceResult> CreateUniversity(UniversityDto dto, string token);
        Task<ServiceResult> UpdateUniversityIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetAllSector(string token);
        Task<List<SectorDto>> GetAllSectorForUI();
        Task<ServiceResult> CreateSector(SectorDto dto, string token);



        Task<ServiceResult> GetTitle();
        Task<ServiceResult> CreateTitle(TitleDto dto);

    }
}
