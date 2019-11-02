using SCA.BLLServices.Generic;
using SCA.DapperRepository.Generic;
using SCA.Entity.Entities;

namespace SCA.BLLServices
{
    public class ContentService : GenericService<Content>, IContentService<Content>
    {
        public ContentService(IGenericRepository<Content> repository) : base(repository)
        {
        }
    }
}
