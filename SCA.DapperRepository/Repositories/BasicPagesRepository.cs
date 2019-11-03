using Microsoft.Extensions.Options;
using SCA.DapperRepository.Generic;
using SCA.Entity.Model;

namespace SCA.DapperRepository
{
    public class BasicPagesRepository : GenericRepository<Entity.Entities.BasicPages>, IBasicPages<Entity.Entities.BasicPages>
    {
        public BasicPagesRepository(IOptions<ConnectionStrings> options) : base(options)
        {
        }
    }
}
