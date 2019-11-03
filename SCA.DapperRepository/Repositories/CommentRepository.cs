using Microsoft.Extensions.Options;
using SCA.DapperRepository.Generic;
using SCA.Entity.Model;

namespace SCA.DapperRepository
{
    public class CommentRepository : GenericRepository<Entity.Entities.Comments>, IComment<Entity.Entities.Comments>
    {
        public CommentRepository(IOptions<ConnectionStrings> options) : base(options)
        {
        }
    }
}
