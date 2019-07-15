using SCA.Common.Result;
using SCA.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services.Interface
{
    public interface IEducationManager
    {
        Task<ServiceResult> GetDepartment();
        Task<ServiceResult> CreateDepartment(DepartmentDto dto);
        Task<ServiceResult> UpdateDepartmentIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetEducationStatus();
        Task<ServiceResult> CreateEducationStatus(EducationStatusDto dto);
        Task<ServiceResult> UpdateEducationStatusIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetFaculty();
        Task<ServiceResult> CreateFaculty(FacultyDto dto);
        Task<ServiceResult> UpdateFacultIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetHighSchool();
        Task<ServiceResult> CreateHighSchool(HighSchoolDto dto);
        Task<ServiceResult> UpdatehighSchoolIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetStudentClass();
        Task<ServiceResult> CreateStudentClass(StudentClassDto dto);
        Task<ServiceResult> UpdateStudentClassIsActive(long Id, bool IsActive);

        Task<ServiceResult> GetUniversity();
        Task<ServiceResult> CreateUniversity(UniversityDto dto);
        Task<ServiceResult> UpdateUniversityIsActive(long Id, bool IsActive);

    }
}
