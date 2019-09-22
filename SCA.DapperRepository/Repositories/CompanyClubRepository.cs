using Microsoft.Extensions.Options;
using SCA.DapperRepository.Generic;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.DapperRepository
{
    public class CompanyClubRepository : GenericRepository<Entity.Entities.CompanyClubs>, ICompanyClub<CompanyClubs>
    {
        public CompanyClubRepository(IOptions<ConnectionStrings> options) : base(options)
        {
        }
    }
}
