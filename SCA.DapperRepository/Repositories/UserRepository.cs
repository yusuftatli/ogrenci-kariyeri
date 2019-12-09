using Microsoft.Extensions.Options;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using SCA.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.DapperRepository
{
    public class UserRepository : GenericRepository<SCA.Entity.Entities.Users>, IUser<SCA.Entity.Entities.Users>
    {
        public UserRepository(IOptions<ConnectionStrings> options) : base(options)
        {
        }
    }
}
