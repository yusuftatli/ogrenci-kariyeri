using Microsoft.Extensions.Options;
using SCA.DapperRepository.Generic;
using SCA.Entity.Model;

namespace SCA.DapperRepository
{
    public class ContentRepository : GenericRepository<Entity.Entities.Content>, IContent<Entity.Entities.Content>
    {
        public ContentRepository(IOptions<ConnectionStrings> options) : base(options)
        {
        }
    }
}
