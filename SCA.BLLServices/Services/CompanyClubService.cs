using SCA.BLLServices.Generic;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.BLLServices
{
    public class CompanyClubService : GenericService<CompanyClubs>, ICompanyClubService<CompanyClubs>
    {
        public CompanyClubService(IGenericRepository<CompanyClubs> repository) : base(repository)
        {
        }
    }
}
