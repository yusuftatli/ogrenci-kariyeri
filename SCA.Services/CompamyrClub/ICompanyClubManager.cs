﻿using SCA.Common.Result;
using SCA.Entity.DTO;
using SCA.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public interface ICompanyClubManager
    {
        Task<ServiceResult> GetAllCompaniesClubs(CompanyClupType companyClupType);
        Task<ServiceResult> GetCompanyId(long id);
        Task<ServiceResult> CreateCompanyClubs(CompanyClubsDto dto, long userId);
    }
}
