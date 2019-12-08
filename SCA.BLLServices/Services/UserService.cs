using SCA.BLLServices.Generic;
using SCA.DapperRepository;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.BLLServices
{
    public class UserService : GenericService<Users>, IUserService<Users>
    {
        public UserService(IGenericRepository<Users> repository) : base(repository)
        {
        }
    }
}
