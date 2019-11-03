using SCA.BLLServices.Generic;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SCA.BLLServices
{
    public class BasicPagesService : GenericService<Entity.Entities.BasicPages>, IBasicPagesService<Entity.Entities.BasicPages>
    {
        public BasicPagesService(IGenericRepository<BasicPages> repository) : base(repository)
        {
        }
    }
}
