using SCA.BLLServices.Generic;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;

namespace SCA.BLLServices
{
    public class CommentService : GenericService<Entity.Entities.Comments>, ICommentService<Entity.Entities.Comments>
    {
        public CommentService(IGenericRepository<Comments> repository) : base(repository)
        {
        }
    }
}
