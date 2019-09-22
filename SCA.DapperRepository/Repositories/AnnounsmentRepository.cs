using Microsoft.Extensions.Options;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.DapperRepository
{
    public class AnnounsmentRepository : GenericRepository<Announsment>, IAnnounsment<Announsment>
    {
        public AnnounsmentRepository(IOptions<ConnectionStrings> options) : base(options)
        {
        }
    }
}
